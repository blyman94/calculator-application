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
        // InputValidator.ValidateInputCount(inputs, 3);
        // InputValidator.ValidateNonZeroInputCount(inputs, 2);
        // InputValidator.ValidateInputSign(inputs, true);

        float a = float.Parse(inputs[0]);
        float b = float.Parse(inputs[1]);
        float c = float.Parse(inputs[2]);

        if (a == 0)
        {
            // The lengths of the hypotenuse (c) and a leg (b) are given,
            // solve for the other leg (a).

            if (b > c)
            {
                throw new InvalidTriangleException("Length of leg b is " +
                    "greater than length of hypotenuse.");
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

            float aSqr = Mathf.Pow(a, 2);
            float cSqr = Mathf.Pow(c, 2);
            float bSqr = cSqr - aSqr;

            return Mathf.Sqrt(bSqr).ToString();
        }
        else
        {
            // c must be = 0.

            // The lengths for both legs (a & b) are given, solve for the 
            // hypotenuse (c).

            float aSqr = Mathf.Pow(a, 2);
            float bSqr = Mathf.Pow(b, 2);
            float cSqr = aSqr + bSqr;

            return Mathf.Sqrt(cSqr).ToString();
        }
    }
    #endregion
}
