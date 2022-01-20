using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Designates a target graphic (image) and a display color type. Allows the 
/// graphic to be painted with a color palette.
/// </summary>
public class ImageColorApplicator : MonoBehaviour, IColorApplicator
{
    /// <summary>
    /// Image to which the color will be applied.
    /// </summary>
    [Tooltip("Image to which the color will be applied.")]
    [SerializeField] private Image targetGraphic;

    /// <summary>
    /// Color from palette to be applied to the target graphic.
    /// </summary>
    [Tooltip("Color from palette to be applied to the target graphic.")]
    [SerializeField] private DisplayColorType displayColorType;

    public void ApplyColor(ColorPalette colorPalette)
    {
        if (targetGraphic != null)
        {
            switch (displayColorType)
            {
                case DisplayColorType.Primary:
                    targetGraphic.color = colorPalette.Primary;
                    break;
                case DisplayColorType.Secondary:
                    targetGraphic.color = colorPalette.Secondary;
                    break;
                case DisplayColorType.Accent:
                    targetGraphic.color = colorPalette.Accent;
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
