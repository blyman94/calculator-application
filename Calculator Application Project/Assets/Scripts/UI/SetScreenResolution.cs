using UnityEngine;

/// <summary>
/// Ensures the resolution is set to the design resolution.
/// </summary>
public class SetScreenResolution : MonoBehaviour
{
    void Start()
    {
        Screen.SetResolution(1080, 1920, true);
    }
}
