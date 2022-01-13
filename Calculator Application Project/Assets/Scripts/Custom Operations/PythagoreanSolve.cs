using UnityEngine;

/// <summary>
/// Responsible for accepting a set of three inputs representing sides of a 
/// right triangle and calculating the length of the missing side (represented 
/// by a 0) of that triangle. 
/// </summary>
[CreateAssetMenu(menuName = "Custom Operation.../Pythagorean Solve",
    fileName = "PythagoreanSolve")]
public class PythagoreanSolve : ScriptableObject, ICustomOperation
{
    /// <summary>
    /// A string with the name of the Pythagorean Solve operation.
    /// </summary>
    [Tooltip("A string containing instructions for the Pythagorean Solve " +
        "operation.")]
    [SerializeField] new private string name;

    /// <summary>
    /// A string containing a description of the Pythagorean Solve operation.
    /// </summary>
    [Tooltip("A string containing a description of the Pythagorean Solve " +
        "operation.")]
    [SerializeField] private string description;

    /// <summary>
    /// A string containing instructions for the Pythagorean Solve operation.
    /// </summary>
    [Tooltip("A string containing instructions for the Pythagorean Solve " +
        "operation.")]
    [SerializeField] private string instructions;

    /// <summary>
    /// An array of strings representing the labels for each input to the 
    /// Pythagorean Solve operation.
    /// </summary>
    [Tooltip("An array of strings representing the labels for each input to " +
        "the Pythagorean Solve operation.")]
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
        float.TryParse(inputs[0], out float a);
        float.TryParse(inputs[1], out float b);
        float.TryParse(inputs[2], out float c);

        if (a == 0)
        {
            // The lengths of the hypotenuse (c) and a leg (b) are given,
            // solve for the other leg (a).

            if (b > c)
            {
                throw new InvalidTriangleException("Invalid Triangle: Length " +
                    "of leg b is greater than length of the hypotenuse.");
            }

            if (b == 0 || c == 0)
            {
                throw new InvalidInputException("Invalid Inputs: Too few " +
                    "inputs given for Pythagorean Solve operation.");
            }

            float bSqr = Mathf.Pow(b, 2);
            float cSqr = Mathf.Pow(c, 2);
            float aSqr = cSqr - bSqr;

            return Mathf.Sqrt(aSqr).ToString();
        }
        else if (b == 0)
        {
            // The lengths of the hypotenuse (c) and a leg (a) are given,
            // solve for the other leg (b).

            if (a > c)
            {
                throw new InvalidTriangleException("Length of leg a is " +
                    "greater than length of hypotenuse.");
            }

            if (a == 0 || c == 0)
            {
                throw new InvalidInputException("Invalid Inputs: Too few " +
                    "inputs given for Pythagorean Solve operation.");
            }

            float aSqr = Mathf.Pow(a, 2);
            float cSqr = Mathf.Pow(c, 2);
            float bSqr = cSqr - aSqr;

            return Mathf.Sqrt(bSqr).ToString();
        }
        else if (c == 0)
        {
            // The lengths for both legs (a & b) are given, solve for the 
            // hypotenuse (c).

            if (a == 0 || b == 0)
            {
                throw new InvalidInputException("Invalid Inputs: Too few " +
                    "inputs given for Pythagorean Solve operation.");
            }

            float aSqr = Mathf.Pow(a, 2);
            float bSqr = Mathf.Pow(b, 2);
            float cSqr = aSqr + bSqr;

            return Mathf.Sqrt(cSqr).ToString();
        }
        else
        {
            // a, b, and c are all populated. Therefore, too many inputs have
            // been given for this operation.
            
            throw new InvalidInputException("Invalid Inputs: Too many " +
                "inputs given for Pythagorean Solve operation.");
        }
    }
    #endregion
}
