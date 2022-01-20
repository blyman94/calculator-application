using UnityEngine;
using TMPro;

/// <summary>
/// Designates a target text and a display color type. Allows the text to be 
/// painted with a color palette.
/// </summary>
public class TextColorApplicator : MonoBehaviour, IColorApplicator
{
    /// <summary>
    /// Image to which the color will be applied.
    /// </summary>
    [Tooltip("Image to which the color will be applied.")]
    [SerializeField] private TextMeshProUGUI targetText;

    /// <summary>
    /// Color from palette to be applied to the target graphic.
    /// </summary>
    [Tooltip("Color from palette to be applied to the target graphic.")]
    [SerializeField] private DisplayColorType displayColorType;

    public void ApplyColor(ColorPalette colorPalette)
    {
        if (targetText != null)
        {
            switch (displayColorType)
            {
                case DisplayColorType.Primary:
                    targetText.color = colorPalette.Primary;
                    break;
                case DisplayColorType.Secondary:
                    targetText.color = colorPalette.Secondary;
                    break;
                case DisplayColorType.Accent:
                    targetText.color = colorPalette.Accent;
                    break;
                default:
                    // Since DisplayColorType is an enum, and the switch 
                    // statement exhausts the list of possible colors, the 
                    // default case will never trigger.
                    break;
            }
        }
    }
}
