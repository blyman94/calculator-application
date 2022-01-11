using TMPro;
using UnityEngine;

/// <summary>
/// Visual representation of an arugment to a custom operation.
/// </summary>
public class ArgumentDisplay : MonoBehaviour
{
    /// <summary>
    /// Text representing the argument's value.
    /// </summary>
    [Tooltip("Text representing the argument's value.")]
    [SerializeField] private TextMeshProUGUI argumentValueText;

    /// <summary>
    /// Text representing the argument's label.
    /// </summary>
    [Tooltip("Text representing the argument's label.")]
    [SerializeField] private TextMeshProUGUI argumentLabelText;

    /// <summary>
    /// Sets the arugment's label text to the passed string.
    /// </summary>
    /// <param name="newText">String to set the argument's label text 
    /// to.</param>
    public void SetArgumentLabelText(string newText)
    {
        argumentLabelText.text = newText;
    }

    /// <summary>
    /// Returns the current value of the argument's label text.
    /// </summary>
    /// <returns>Returns the current value of the argument's label 
    /// text.</returns>
    public string GetArgumentValueText()
    {
        return argumentValueText.text;
    }

    /// <summary>
    /// Sets the arugment's value text to the passed string.
    /// </summary>
    /// <param name="newText">String to set the argument's value text 
    /// to.</param>
    public void SetArgumentValueText(string newText)
    {
        argumentLabelText.text = newText;
    }
}
