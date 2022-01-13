using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    /// Image to change color of to signal activation state.
    /// </summary>
    [Tooltip("Image to change color of to signal activation state.")]
    [SerializeField] private Image activeStateImage;

    /// <summary>
    /// Color to highlight the argument value with when active.
    /// </summary>
    [Tooltip("Color to highlight the argument value with when active.")]
    [SerializeField] private Color activeColor;

    /// <summary>
    /// Color to highlight the argument value with when inactive.
    /// </summary>
    [Tooltip("Color to highlight the argument value with when inactive.")]
    [SerializeField] private Color inactiveColor;

    #region MonoBehaviour Methods
    private void Awake()
    {
        activeStateImage.color = inactiveColor;
    }
    #endregion

    /// <summary>
    /// Visually identifies this argument display as the active argument the
    /// user can edit while executing a custom operation.
    /// </summary>
    public void Activate()
    {
        activeStateImage.color = activeColor;
    }

    /// <summary>
    /// Visually identifies this argument display as an inactive argument the
    /// user is not currently editing while executing a custom operation.
    /// </summary>
    public void Deactivate()
    {
        activeStateImage.color = inactiveColor;
    }
    
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
        argumentValueText.text = newText;
    }
}
