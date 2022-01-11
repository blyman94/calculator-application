using UnityEngine;

/// <summary>
/// Responsible for accepting a single input (n) representing a number to apply 
/// the factorial operator (!) to and returning the factorial of the input (n!). 
/// </summary>
[CreateAssetMenu(menuName = "Custom Operation.../Factorial",
    fileName = "Factorial")]
public class Factorial : ScriptableObject, ICustomOperation
{
    /// <summary>
    /// A string with the name of the Factorial operation.
    /// </summary>
    [Tooltip("A string containing instructions for the Factorial " +
        "operation.")]
    [SerializeField] new private string name;

    /// <summary>
    /// A string containing a description of the Factorial operation.
    /// </summary>
    [Tooltip("A string containing a description of the Factorial " +
        "operation.")]
    [SerializeField] private string description;

    /// <summary>
    /// A string containing instructions for the Factorial operation.
    /// </summary>
    [Tooltip("A string containing instructions for the Factorial " +
        "operation.")]
    [SerializeField] private string instructions;

    /// <summary>
    /// An array of strings representing the labels for each input to the 
    /// Factorial operation.
    /// </summary>
    [Tooltip("An array of strings representing the labels for each input to " +
        "the Factorial operation.")]
    [SerializeField] private string[] argumentLabels;

    #region ICustomOperation Methods
    public string Description
    {
        get
        {
            return description;
        }
    }

    public string[] ArgumentLabels
    {
        get
        {
            return argumentLabels;
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
        // InputValidator.ValidateInputCount(inputs, 1);
        // InputValidator.ValidateInputSign(inputs, true);
        // InputValidator.ValidateInputInteger(inputs[0]);

        int n = int.Parse(inputs[0]);

        if (n == 0)
        {
            return "1";
        }
        else
        {
            return CalculateFactorial(n).ToString();
        }
    }

    /// <summary>
    /// Uses recursion to calculate the factorial of a given integer n. The exit
    /// condition triggers when n is equal to 1.
    /// </summary>
    /// <param name="n">The integer for which the factorial value will be 
    /// calculated.</param>
    /// <returns>An integer representing the factorial value of the given 
    /// integer n.</returns>
    private int CalculateFactorial(int n)
    {
        if (n == 1)
        {
            return 1;
        }
        else
        {
            return n * CalculateFactorial(n - 1);
        }
    }
    #endregion
}
