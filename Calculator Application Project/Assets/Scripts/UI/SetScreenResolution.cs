using UnityEngine;

/// <summary>
/// Ensures the resolution is set to the design resolution. This script should
/// only be activated during the Android build process.
/// </summary>
public class SetScreenResolution : MonoBehaviour
{
    void Start()
    {
#if UNITY_ANDROID
        Screen.SetResolution(1080, 1920, true);
#endif

#if UNITY_STANDALONE
        Screen.SetResolution(720, 1280, false);
#endif

#if UNITY_WEBGL
        Screen.SetResolution(360, 640, false);
#endif
    }
}
