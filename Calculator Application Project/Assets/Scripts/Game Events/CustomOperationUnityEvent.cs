using System;
using UnityEngine.Events;

/// <summary>
/// Extension of the UnityEvent class that allows for passage of an 
/// ICustomOperation.
/// </summary>
[Serializable]
public class CustomOperationUnityEvent : UnityEvent<ICustomOperation>
{
    
}