using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The set of IColorApplicator elements to be invoked at startup.
/// </summary>
[CreateAssetMenu]
public class ColorApplicatorSet : ScriptableObject
{
    /// <summary>
    /// Color palette to pass to constituent applicators.
    /// </summary>
    public ColorPalette ColorPalette
    {
        get
        {
            return colorPallete;
        }
        set
        {
            colorPallete = value;
            foreach (IColorApplicator colorApplicator in colorApplicators)
            {
                colorApplicator.ApplyColor(colorPallete);
            }
        }
    }

    /// <summary>
    /// List of constituent colorApplicators.
    /// </summary>
    public List<IColorApplicator> colorApplicators { get; set; } =
        new List<IColorApplicator>();

    /// <summary>
    /// Color palette to pass to constituent applicators.
    /// </summary>
    [Tooltip("Color palette to pass to constituent applicators.")]
    [SerializeField] private ColorPalette colorPallete;

    /// <summary>
    /// Adds an IColorApplicator to the set. Applies the color palette.
    /// </summary>
    /// <param name="colorApplicator">IColorApplicator to add.</param>
    public void Add(IColorApplicator colorApplicator)
    {
        colorApplicators.Add(colorApplicator);
        colorApplicator.ApplyColor(colorPallete);
    }

    /// <summary>
    /// Removes an IColorApplicator from the set.
    /// </summary>
    /// <param name="colorApplicator">IColorApplicator tp remove.</param>
    public void Remove(IColorApplicator colorApplicator)
    {
        if (colorApplicators.Contains(colorApplicator))
        {
            colorApplicators.Remove(colorApplicator);
        }
    }
}
