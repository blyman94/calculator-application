using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject event that passes an ICustomOperation to it's listeners 
/// when raised.
/// </summary>
[CreateAssetMenu]
public class CustomOperationEvent : ScriptableObject
{
    /// <summary>
    /// List of listeners interested in this event. When the event is raised,
    /// each listener's "OnEventRaised(ICustomOperation)" method will be called.
    /// </summary>
    private List<CustomOperationEventListener> listeners =
        new List<CustomOperationEventListener>();

    /// <summary>
    /// Signals to listeners that the event has been raised and passes to them
    /// an ICustomOperation
    /// </summary>
    /// <param name="customOperation">ICustomOperation to pass to 
    /// listeners.</param>
    public void Raise(ICustomOperation customOperation)
    {
        if (listeners.Count > 0)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(customOperation);
            }
        }
    }

    /// <summary>
    /// Registers a listener to this event, such that the listener responds when
    /// the event is raised.
    /// </summary>
    /// <param name="listener">CustomOperationEventListener to be added to this
    /// event's listener list.</param>
    public void RegisterListener(CustomOperationEventListener listener)
    {
        listeners.Add(listener);
    }

    /// <summary>
    /// Unregisters (removes) a listener to this event, such that the listener 
    /// no longer responds when the event is raised.
    /// </summary>
    /// <param name="listener">CustomOperationEventListener to be removed from 
    /// this event's listener list.</param>
    public void UnregisterListener(CustomOperationEventListener listener)
    {
        if (listeners.Contains(listener))
        {
            listeners.Remove(listener);
        }
    }
}
