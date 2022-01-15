using UnityEngine;


public abstract class CustomOperation : ScriptableObject, ICustomOperation
{
    /// <summary>
    /// A string with the name of the custom operation.
    /// </summary>
    [Tooltip("A string containing instructions for the custom " +
        "operation.")]
    [SerializeField] new protected string name;

    /// <summary>
    /// A string containing a description of the custom operation.
    /// </summary>
    [Tooltip("A string containing a description of the custom " +
        "operation.")]
    [SerializeField] protected string description;

    /// <summary>
    /// A string containing instructions for the custom operation.
    /// </summary>
    [Tooltip("A string containing instructions for the custom " +
        "operation.")]
    [SerializeField] protected string instructions;

    /// <summary>
    /// An array of strings representing the labels for each input to the 
    /// custom operation.
    /// </summary>
    [Tooltip("An array of strings representing the labels for each input to " +
        "the custom operation.")]
    [SerializeField] protected string[] argumentLabels;

    /// <summary>
    /// Can this custom operation accept decimal (non-integer) values?
    /// </summary>
    [Tooltip("Can this custom operation accept decimal (non-integer) values?")]
    [SerializeField] protected bool allowsDecimal;

    /// <summary>
    /// Can this custom operation accept negative values?
    /// </summary>
    [Tooltip("Can this custom operation accept negative values?")]
    [SerializeField] protected bool allowsNegative;

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

    public abstract string Execute(string[] inputs);

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
    #endregion
}
