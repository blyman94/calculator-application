namespace CalculatorLibrary;

/// <summary>
/// Responsible for accepting a set of three inputs representing sides of a 
/// right triangle and calculating the length of the missing side (represented 
/// by a 0) of that triangle. 
/// </summary>
public class PythagoreanSolver : ICustomOperation
{
    /// <summary>
    /// Parameterless constructor for the PythagoreanSolver class. 
    /// </summary>
    public PythagoreanSolver()
    {

    }
    
    public string Execute(string[] inputs)
    {
        InputValidator.ValidateInputCount(inputs, 3);
        InputValidator.ValidateNonZeroInputCount(inputs, 2);
        InputValidator.ValidateInputSign(inputs, true);

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

            float bSqr = MathF.Pow(b, 2);
            float cSqr = MathF.Pow(c, 2);
            float aSqr = cSqr - bSqr;

            return MathF.Sqrt(aSqr).ToString();
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

            float aSqr = MathF.Pow(a, 2);
            float cSqr = MathF.Pow(c, 2);
            float bSqr = cSqr - aSqr;

            return MathF.Sqrt(bSqr).ToString();
        }
        else
        {
            // c must be = 0.

            // The lengths for both legs (a & b) are given, solve for the 
            // hypotenuse (c).

            float aSqr = MathF.Pow(a, 2);
            float bSqr = MathF.Pow(b, 2);
            float cSqr = aSqr + bSqr;

            return MathF.Sqrt(cSqr).ToString();
        }
    }

    
}