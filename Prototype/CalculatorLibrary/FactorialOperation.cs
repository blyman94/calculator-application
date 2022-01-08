namespace CalculatorLibrary;

/// <summary>
/// Responsible for accepting a single input (n) representing a number to apply 
/// the factorial operator (!) to and returning the factorial of the input (n!). 
/// </summary>
public class FactorialOperation : ICustomOperation
{
    /// <summary>
    /// Parameterless constructor for the FactorialOperation class. 
    /// </summary>
    public FactorialOperation()
    {

    }
    
    public string Execute(string[] inputs)
    {
        InputValidator.ValidateInputCount(inputs,1);
        InputValidator.ValidateInputSign(inputs,true);
        InputValidator.ValidateInputInteger(inputs[0]);

        int n = int.Parse(inputs[0]);

        if (n == 0)
        {
            return "1";
        }
        else
        {
            return Factorial(n).ToString();
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
    private int Factorial(int n)
    {
        if (n == 1)
        {
            return 1;
        }
        else
        {
            return n * Factorial(n - 1);
        }
    }
}