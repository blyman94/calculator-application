using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Describes a color applicator, an object that applies a color to a UI
/// element.
/// </summary>
public interface IColorApplicator
{
    /// <summary>
    /// Applies the selected color to the target graphic.
    /// </summary>
    void ApplyColor(ColorPalette colorPalette);
}
