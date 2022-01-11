using UnityEngine;

/// <summary>
/// Class to be attached to GameObjects, allowing them to respond to specific
/// CustomOperationEvent raise calls. CustomOperationUnityEvent Responses can be 
/// configured in the inspector, but the scope of all references should be 
/// within a single GameObject.
/// </summary>
public class CustomOperationEventListener : MonoBehaviour
{
    /// <summary>
    /// Event that this listener will listen to.
    /// </summary>
    [Tooltip("Event that this listener will listen to.")]
    public CustomOperationEvent Event;

    /// <summary>
    /// Responses to trigger when the event is raised.
    /// </summary>
    [Tooltip("Responses to trigger when the event is raised.")]
    public CustomOperationUnityEvent Response;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        Event.RegisterListener(this);
    }
    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }
    #endregion

    /// <summary>
    /// Responds the the assigned event's raise call by invoking a Unity Event.
    /// </summary>
    public void OnEventRaised(ICustomOperation customOperation)
    {
        Response.Invoke(customOperation);
    }
}
