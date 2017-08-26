using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using Debug = UnityEngine.Debug;

public class MobileBuildHelper
{
    private const string BUILD_CANCELLED = "Building Player was cancelled";

    [MenuItem("Build Helper/Push To Android _&b")]
    public static void PushToAndroid()
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            EditorUtility.DisplayDialog("Push To Android", "Android only!", "ok...");

            return;
        }

        var apkLocation = EditorUserBuildSettings.GetBuildLocation(EditorUserBuildSettings.activeBuildTarget);

        if (string.IsNullOrEmpty(apkLocation) || !File.Exists(apkLocation))
        {
            apkLocation = EditorUtility.OpenFilePanel("Find APK", Environment.CurrentDirectory, "apk");
        }

        if (string.IsNullOrEmpty(apkLocation) || !File.Exists(apkLocation))
        {
            Debug.LogError("Cannot find .apk file.");
            return;
        }

        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "adb",
            Arguments = string.Format("install -r \"{0}\"", apkLocation)
        });

        if (process != null)
        {
            process.WaitForExit();

            Process.Start(new ProcessStartInfo
            {
                FileName = "adb",
                Arguments = string.Format("shell am start -n \"{0}/{1}\"", PlayerSettings.applicationIdentifier, "com.unity3d.player.UnityPlayerNativeActivity")
            });
        }
        else
        {
            throw new Exception("Failed to run adb command");
        }
    }

    [MenuItem("Build Helper/Extract Keys From Keystore")]
    public static void ExtractKeysFromKeystore()
    {
        ValidateKeystoreData();

        var cmd1 = string.Format(@"keytool -exportcert -keystore ""{0}"" -alias {1} -storepass {2} -list -v ", PlayerSettings.Android.keystoreName, PlayerSettings.Android.keyaliasName, PlayerSettings.Android.keystorePass);
        var cmd2 = string.Format(@"keytool -exportcert -keystore ""{0}"" -alias {1} -storepass {2} -list -v | openssl sha1 -binary | openssl base64", PlayerSettings.Android.keystoreName, PlayerSettings.Android.keyaliasName, PlayerSettings.Android.keystorePass);

        var process = Process.Start(new ProcessStartInfo
        {
            FileName = "cmd",
            Arguments = string.Format("/C {0}&{1}", cmd1, cmd2),
            RedirectStandardOutput = true,
            UseShellExecute = false
        });

        if (process != null)
        {
            process.WaitForExit();

            var output = process.StandardOutput.ReadToEnd();

            var sha1 = Regex.Match(output, "SHA1: \\S*").Value.Trim('\n');

            cmd1 = "echo " + sha1;
            cmd2 = "echo HASH: " + output.Split('\n').Last();

            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = string.Format("/C {0}&{1}&pause", cmd1, cmd2)
            });
        }
    }

    private enum BuildType
    {
        Debug,
        Release
    }

    [MenuItem("Build Helper/Build (Debug) _%#&b")]
    public static void BuildDebug()
    {
        Build(BuildType.Debug);
    }

    [MenuItem("Build Helper/Build (Release)")]
    public static void BuildRelease()
    {
        Build(BuildType.Release);
    }

    private static void Build(BuildType buildType)
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android &&
#if UNITY_5
            EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
#else
            EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
#endif
        {
            EditorUtility.DisplayDialog("Android + iOS Build Helper", "Only Android and iOS are supported, you're targeting: " + EditorUserBuildSettings.activeBuildTarget, "ok...");

            return;
        }

        Action extraActionsOnSuccessfulBuild = () => { };

        Version version;

        try
        {
            version = new Version(PlayerSettings.bundleVersion);
        }
        catch (Exception)
        {
            version = new Version(0, 1, 0);
        }

        var versionBuild = version.Build;
        var versionMinor = version.Minor;

        if (buildType == BuildType.Debug)
        {
            versionBuild++;
        }
        else
        {
            versionMinor++;
        }

        version = new Version(version.Major, versionMinor, versionBuild);

        extraActionsOnSuccessfulBuild += () => PlayerSettings.bundleVersion = version.ToString();

        var fileName = PlayerSettings.productName;

        int? originalVersionCode = null;

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            if (buildType == BuildType.Release)
            {
                originalVersionCode = PlayerSettings.Android.bundleVersionCode++;
            }

            fileName += string.Format("({0})", PlayerSettings.Android.bundleVersionCode);

            ValidateKeystoreData();
        }

        var folderPath = string.Format("{0}/{1}", FileExtension, buildType);

        Directory.CreateDirectory(folderPath);

        fileName = string.Format("{0}/{1}_{2}.{3}", folderPath, fileName, version, FileExtension);

        var buildLocation = new FileInfo(fileName).FullName;

        Debug.Log("Building: " + buildLocation);

        var isBadBuild = true;

        if (EditorUtility.DisplayDialog(buildType + " Build", "Building: " + fileName.Replace(folderPath, string.Empty), "Ok go :)"))
        {
            var result = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes.Select(s => s.path).ToArray(),
                                                   buildLocation, 
                                                   EditorUserBuildSettings.activeBuildTarget, 
                                                   GetCurrentBuildSettings(buildType));
            if (string.IsNullOrEmpty(result))
            {
                isBadBuild = false;

                result = "Success!";

                extraActionsOnSuccessfulBuild();

                OnSuccessfulBuild(buildLocation);
            }
            else if (result == BUILD_CANCELLED)
            {
                result = "Cancelled...";
            }

            if (result.ToLower().Contains("error"))
            {
                Debug.LogError(buildType + " build ERROR: " + result);
            }
            else
            {
                Debug.Log(buildType + " build result: " + result);
            }

            EditorUtility.DisplayDialog(buildType + " build result", result, "OK, go away..");
        }
        else
        {
            Debug.Log(buildType + " build was cancelled...");
        }

        if (isBadBuild && buildType == BuildType.Release && originalVersionCode.HasValue)
        {
            PlayerSettings.Android.bundleVersionCode = originalVersionCode.Value;
        }
    }

    private static void ValidateKeystoreData()
    {
        if (string.IsNullOrEmpty(PlayerSettings.Android.keystoreName))
        {
            HandleError("Missing keystore file!");
        }

        var keystore = new FileInfo(PlayerSettings.Android.keystoreName);

        if (!keystore.Exists)
        {
            keystore = new FileInfo(keystore.Name);
        }

        if (!keystore.Exists)
        {
            HandleError("Keystore file not found: " + keystore.FullName);
        }

        PlayerSettings.Android.keystoreName = keystore.FullName;

        if (string.IsNullOrEmpty(PlayerSettings.Android.keyaliasName))
        {
            HandleError("Missing keystore alias!");
        }

        if (string.IsNullOrEmpty(PlayerSettings.Android.keystorePass))
        {
            HandleError("Missing keystore password!");
        }

        if (string.IsNullOrEmpty(PlayerSettings.Android.keyaliasPass))
        {
            HandleError("Missing keystore alias password!");
        }
    }

    private static BuildOptions GetCurrentBuildSettings(BuildType buildType)
    {
        var buildSettings = BuildOptions.None;

        if (EditorUserBuildSettings.allowDebugging && buildType == BuildType.Debug)
        {
            buildSettings &= BuildOptions.AllowDebugging;
        }

        if (EditorUserBuildSettings.development && buildType == BuildType.Debug)
        {
            buildSettings &= BuildOptions.Development;
        }

        if (EditorUserBuildSettings.connectProfiler && buildType == BuildType.Debug)
        {
            buildSettings &= BuildOptions.ConnectWithProfiler;
        }

        if (EditorUserBuildSettings.enableHeadlessMode)
        {
            buildSettings &= BuildOptions.EnableHeadlessMode;
        }

        if (EditorUserBuildSettings.installInBuildFolder)
        {
            buildSettings &= BuildOptions.InstallInBuildFolder;
        }

        if (EditorUserBuildSettings.symlinkLibraries)
        {
            buildSettings &= BuildOptions.SymlinkLibraries;
        }

        return buildSettings;
    }

    private static void OnSuccessfulBuild(string buildLocation)
    {
        EditorUserBuildSettings.SetBuildLocation(EditorUserBuildSettings.activeBuildTarget, buildLocation);

        if (EditorWindow.focusedWindow)
        {
            EditorWindow.focusedWindow.Repaint();
        }

        Process.Start(new ProcessStartInfo
        {
            FileName = "explorer",
            Arguments = string.Format("\"{0}\"", new FileInfo(buildLocation).Directory)
        });

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android &&
            EditorUtility.DisplayDialogComplex("Deploy", "Would you like to push the new apk to the connected device?", "Yes please :)", "Go away!", "No thanks") == 0)
        {
            PushToAndroid();
        }
    }

    public static string FileExtension
    {
        get
        {
            return EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android ? "APK"
                 : EditorUserBuildSettings.activeBuildTarget ==
#if UNITY_5
                 BuildTarget.iOS
#else
                 BuildTarget.iOS
#endif
                  ? "IPA" : string.Empty;
        }
    }

    private static void HandleError(string error)
    {
        EditorUtility.DisplayDialog("Error", error, "OK");

        throw new Exception(error);
    }
}
