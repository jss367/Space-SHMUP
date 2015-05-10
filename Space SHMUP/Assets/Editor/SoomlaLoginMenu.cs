using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SoomlaLoginMenu : ScriptableObject
{
    [MenuItem("Window/Soomla/Sign up or Login")]
    public static void OpenLoginOrSignUpPage()
    {
        Application.OpenURL("https://doorman.soom.la/oauth/authorize?response_type=code&redirect_uri=http%3A%2F%2Fdashboard.soom.la%2Fauth%2Fdoorman%2Fcallback&scope=own&client_id=growDashboard&referrer=Spartonix");
    }
}