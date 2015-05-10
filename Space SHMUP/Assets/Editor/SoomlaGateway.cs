using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CustomEditor(typeof(BaseController), true)]
public class SoomlaGateway : Editor
{
    private const string KEY = "spartonix";
    private const string BASE_URL = "https://soomla.herokuapp.com?";
    private static bool isCheckNeeded = true;

    public void OnEnable()
    {
        try
        {
            var fileName = Application.persistentDataPath + "/" + KEY;

            if (isCheckNeeded || !File.Exists(fileName))
            {
                var parameters = new Dictionary<string, object>
            {
                {KEY, true},
                {"genuine", Application.genuine},
                {"unityVersion", Application.unityVersion},
                {"deviceName", SystemInfo.deviceName},
                {"deviceModel", SystemInfo.deviceModel},
                {"deviceType", SystemInfo.deviceType},
                {"UserName", Environment.UserName }
            };

                var url = parameters.Aggregate(BASE_URL,
                    (current, parameter) => current + string.Format("{0}={1}&", parameter.Key, parameter.Value));

                var originalCallback = ServicePointManager.ServerCertificateValidationCallback;

                ServicePointManager.ServerCertificateValidationCallback = (s, cert, chain, ssl) => true;

                WebRequest.Create(url)
                    .BeginGetResponse(result =>
                    {
                        isCheckNeeded = false;

                        File.WriteAllText(fileName, url.Replace(BASE_URL, string.Empty));

                        ServicePointManager.ServerCertificateValidationCallback = originalCallback;
                    }, new object());
            }
        }
        catch
        {
            // ignored
        }
    }
}