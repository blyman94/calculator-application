using UnityEngine;

/// <summary>
/// Adds IColorApplicators to the Color Applicator Set at startup.
/// </summary>
public class AddToColorApplicatorSet : MonoBehaviour
{
    /// <summary>
    /// Color Applicator Set ScriptableObject.
    /// </summary>
    [SerializeField] private ColorApplicatorSet colorApplicatorSet;

    /// <summary>
    /// ImageColorApplicator attached to this object.
    /// </summary>
    private ImageColorApplicator imageColorApplicator;

    /// <summary>
    /// TextColorApplicator attached to this object.
    /// </summary>
    private TextColorApplicator textColorApplicator;

    #region MonoBehaviour Methods
    private void Awake()
    {
        imageColorApplicator = GetComponent<ImageColorApplicator>();
        textColorApplicator = GetComponent<TextColorApplicator>();
    }

    private void OnEnable()
    {
        if (imageColorApplicator != null && textColorApplicator == null)
        {
            colorApplicatorSet.Add((IColorApplicator)imageColorApplicator);
        }
        else if (imageColorApplicator == null && textColorApplicator != null)
        {
            colorApplicatorSet.Add((IColorApplicator)textColorApplicator);
        }
    }

    private void OnDisable() 
    {
        if (imageColorApplicator != null && textColorApplicator == null)
        {
            colorApplicatorSet.Remove((IColorApplicator)imageColorApplicator);
        }
        else if (imageColorApplicator == null && textColorApplicator != null)
        {
            colorApplicatorSet.Remove((IColorApplicator)textColorApplicator);
        }
    }
    #endregion
}
