using UnityEngine;

/// <summary>
/// Responsible for accepting a set of three inputs representing sides of a 
/// right triangle and calculating the length of the missing side (represented 
/// by a 0) of that triangle. 
/// </summary>
[CreateAssetMenu(menuName = "Custom Operation.../Pythagorean Solve",
    fileName = "PythagoreanSolve")]
public class PythagoreanSolve : CustomOperation
{
    public override string Execute(string[] inputs)
    {
        float.TryParse(inputs[0], out float a);
        float.TryParse(inputs[1], out float b);
        float.TryParse(inputs[2], out float c);

        if (a == 0)
        {
            // The lengths of the hypotenuse (c) and a leg (b) are given,
            // solve for the other leg (a).
            if (b == 0 || c == 0)
            {
                throw new InvalidInputException("Invalid Inputs: Too few " +
                    "inputs given for Pythagorean Solve operation.");
            }

            if (b > c)
            {
                throw new InvalidTriangleException("Invalid Triangle: Length " +
                    "of leg b is greater than length of the hypotenuse.");
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

            if (a == 0 || c == 0)
            {
                throw new InvalidInputException("Invalid Inputs: Too few " +
                    "inputs given for Pythagorean Solve operation.");
            }

            if (a > c)
            {
                throw new InvalidTriangleException("Invalid Triangle: Length " +
                    "of leg a is greater than length of hypotenuse.");
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
}
