using UnityEditor;
using UnityEngine;

public class PlayerPrefsCleaner
{
    [MenuItem("PlayerPrefs/Delete All")]
    public static void Clean()
    {
        PlayerPrefs.DeleteAll();
    }
}
