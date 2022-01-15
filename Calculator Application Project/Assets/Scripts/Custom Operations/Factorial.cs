using UnityEngine;

/// <summary>
/// Responsible for accepting a single input (n) representing a number to apply 
/// the factorial operator (!) to and returning the factorial of the input (n!). 
/// </summary>
[CreateAssetMenu(menuName = "Custom Operation.../Factorial",
    fileName = "Factorial")]
public class Factorial : CustomOperation
{
    #region ICustomOperation Methods
    public override string Execute(string[] inputs)
    {
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
    #endregion

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
}
