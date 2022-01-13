using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for holding information about a selected custom operation and 
/// passing the information when selected.
/// </summary>
public class OperationSelectionButton : MonoBehaviour
{
    [Header("Events")]
    /// <summary>
    /// Used to signal that the user has selected a custom operation.
    /// </summary>
    [Tooltip("Used to signal that the user has selected a custom operation.")]
    [SerializeField] private CustomOperationEvent customOperationEvent;

    [Header("GUI Elements")]
    /// <summary>
    /// Text that will display the name of the operation being selected.
    /// </summary>
    [Tooltip("Text that will display the name of the operation being " +
        "selected.")]
    [SerializeField] private TextMeshProUGUI labelText;

    /// <summary>
    /// Custom Operation information this button selection represents.
    /// </summary>
    private ICustomOperation customOperation;

    /// <summary>
    /// Custom Operation information this button selection represents.
    /// </summary>
    public ICustomOperation CustomOperation
    {
        get
        {
            return customOperation;
        }
        set
        {
            customOperation = value;
            labelText.text = customOperation.Name;
        }
    }

    /// <summary>
    /// Signals a custom operation has been selected.
    /// </summary>
    public void RaiseCustomOperationEvent()
    {
        if (customOperationEvent != null && CustomOperation != null)
        {
            customOperationEvent.Raise(CustomOperation);
        }
    }
}
