using UnityEngine;
using Soomla.Highway;

public class HighwayInitializer : MonoBehaviour
{
    protected void Start()
    {
        // Highway only works on Android or iOS devices
        if (Application.platform == RuntimePlatform.Android || 
            Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SoomlaStorageInitializer.Initialize();

            // Make sure to make this call in your earlieast loading scene,
            // and before initializing any other SOOMLA components
            // i.e. before SoomlaStore.Initialize(...)
            SoomlaHighway.Initialize();
        }
    }
}
