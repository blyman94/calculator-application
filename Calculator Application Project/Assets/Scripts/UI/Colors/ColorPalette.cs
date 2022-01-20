using UnityEngine;

/// <summary>
/// Stores a primary color, secondary color, and accent color for reference by
/// UI images.
/// </summary>
[CreateAssetMenu]
public class ColorPalette : ScriptableObject
{
    /// <summary>
    /// Primary color of the palette.
    /// </summary>
    [Tooltip("Primary color of the palette.")]
    public Color Primary;

    /// <summary>
    /// Secondary color of the palette.
    /// </summary>
    [Tooltip("Secondary color of the palette.")]
    public Color Secondary;

    /// <summary>
    /// Accent color of the palette.
    /// </summary>
    [Tooltip("Accent color of the palette.")]
    public Color Accent;
}
