using UnityEngine;

/// <summary>
/// Responsible for accepting a single input representing a length in inches and
/// returning the length converted to centimeters.
/// </summary>
[CreateAssetMenu(menuName = "Custom Operation.../Metric Converter",
    fileName = "MetricConverter")]
public class MetricConverter : ScriptableObject, ICustomOperation
{
    /// <summary>
    /// A string with the name of the Metric Converter operation.
    /// </summary>
    [Tooltip("A string containing instructions for the Metric Converter " +
        "operation.")]
    [SerializeField] new private string name;

    /// <summary>
    /// A string containing a description of the Metric Converter operation.
    /// </summary>
    [Tooltip("A string containing a description of the Metric Converter " +
        "operation.")]
    [SerializeField] private string description;

    /// <summary>
    /// A string containing instructions for the Metric Converter operation.
    /// </summary>
    [Tooltip("A string containing instructions for the Metric Converter " +
        "operation.")]
    [SerializeField] private string instructions;

    /// <summary>
    /// An array of strings representing the labels for each input to the 
    /// Metric Converter operation.
    /// </summary>
    [Tooltip("An array of strings representing the labels for each input to " +
        "the Metric Converter operation.")]
    [SerializeField] private string[] argumentLabels;

    /// <summary>
    /// Can this custom operation accept decimal (non-integer) values?
    /// </summary>
    [Tooltip("Can this custom operation accept decimal (non-integer) values?")]
    [SerializeField] private bool allowsDecimal;

    /// <summary>
    /// Can this custom operation accept negative values?
    /// </summary>
    [Tooltip("Can this custom operation accept negative values?")]
    [SerializeField] private bool allowsNegative;

    #region ICustomOperation Methods
    public bool AllowsDecimal
    {
        get
        {
            return allowsDecimal;
        }
    }
    
    public bool AllowsNegative
    {
        get
        {
            return allowsNegative;
        }
    }

    public string[] ArgumentLabels
    {
        get
        {
            return argumentLabels;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public string Instructions
    {
        get
        {
            return instructions;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
    }

    public string Execute(string[] inputs)
    {
        float impLength = float.Parse(inputs[0]);
        return (impLength * 2.54f).ToString();
    }
    #endregion
}
