using UnityEngine;

/// <summary>
/// Allows the user to quit the application.
/// </summary>
[CreateAssetMenu]
public class ApplicationManager : ScriptableObject
{
    /// <summary>
    /// Quits the application. This method only functions when the application 
    /// is built.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}
