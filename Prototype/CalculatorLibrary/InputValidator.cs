namespace CalculatorLibrary;

/// <summary>
/// Contains helper methods that aid in ensuring that incoming input meets 
/// the expectations of the operations using the input. Contains several 
/// public methods that test input against various criteria throughout 
/// the application.
/// </summary>
public static class InputValidator
{
    /// <summary>
    /// Determines if the passed string contains leading zeroes. Leading
    /// zeroes are defined as more than one zero to the left of a decimal
    /// point with no non-zero integer separating them.
    /// </summary>
    /// <param name="value">String to be evaluated for leading zero 
    /// content.</param>
    /// <returns>A boolean value. True if the value to be evaluated contains
    /// leading zeros. False otherwise.</returns>
    public static bool ContainsLeadingZeros(string value)
    {
        if (value == "")
        {
            return false;
        }

        return value.Substring(0, 2) == "00";
    }

    /// <summary>
    /// Compares the passed string's numerical value to zero. 
    /// </summary>
    /// <param name="value">Value to be compared</param>
    /// <param name="orEqualTo">Boolean determining if, when the comparison 
    /// occurs, 0 should be included.</param>
    /// <returns>A boolean value. True if the string's numerical value is 
    /// greater than zero. Also true if the string's numerical value is equal 
    /// to zero and the "orEqualTo" bool parameter is true. False 
    /// otherwise.</returns>
    public static bool CompareToZero(string value, bool orEqualTo)
    {
        if (orEqualTo)
        {
            return float.Parse(value) >= 0;
        }
        else
        {
            return float.Parse(value) > 0;
        }
    }

    /// <summary>
    /// Ensures the length of the input array is equal to an expected array
    /// length. Throws an exception if the length of the input array is not 
    /// equal to the expected length.
    /// </summary>
    /// <param name="inputs">String array representing all inputs.</param>
    /// <param name="expectedCount">Expected count of inputs.</param>
    /// <exception cref="InvalidInputException">Thrown when the count of inputs
    /// is not equal to the expected count.</exception>
    public static void ValidateInputCount(string[] inputs, int expectedCount)
    {
        if (inputs.Length != expectedCount)
        {
            throw new InvalidInputException("The input array should be " +
                $"of length {expectedCount}.");
        }
    }

    /// <summary>
    /// Ensures that all non-zero inputs in a string array are greater than 
    /// zero.
    /// </summary>
    /// <param name="inputs">String array representing inputs to be 
    /// evaluated</param>
    /// <exception cref="InvalidInputException">Thrown when an input is non-zero
    /// and is less than 0.</exception>
    public static void ValidateInputSign(string[] inputs, bool allowZero)
    {
        foreach (string input in inputs)
        {
            if (allowZero)
            {
                if (!InputValidator.CompareToZero(input, true))
                {
                    throw new InvalidInputException("All non-zero " +
                        "elements must be positive.");
                }
            }
            else
            {
                if (!InputValidator.CompareToZero(input, false))
                {
                    throw new InvalidInputException("All elements must be " +
                        "positive.");
                }
            }
        }
    }

    /// <summary>
    /// Ensures the single input string custom represents an integer. Throws an 
    /// exception if the input string does not represent an integer (contains a 
    /// decimal point).
    /// </summary>
    /// <param name="inputs">String value to be evaluated for integer status.
    /// </param>
    /// <exception cref="InvalidInputException">Thrown if input is not an 
    /// integer.</exception>
    public static void ValidateInputInteger(string input)
    {
        bool isInteger = int.TryParse(input, out int value);
        if (!isInteger)
        {
            throw new InvalidInputException("The input must be an integer.");
        }
    }

    /// <summary>
    /// Ensures the number of non-zero inputs in the passed input array is equal
    /// to the expected count of non-zero inputs. Throws an exception if the 
    /// number of non-zero inputs is not equal to the expected count of non-zero
    /// inputs.
    /// </summary>
    /// <param name="inputs">String array representing all inputs to the
    /// Pythagorean Solve custom operation.</param>
    /// <param name="expectedCount">Expected count of non-zero inputs.</param>
    /// <exception cref="InvalidInputException">Thrown when the number of non-
    /// zero inputs is not equal to the expected count.</exception>
    public static void ValidateNonZeroInputCount(string[] inputs,
        int expectedCount)
    {
        string[] nonZeroInputs = inputs.Where(e => e != "0").ToArray();
        if (nonZeroInputs.Length != expectedCount)
        {
            throw new InvalidInputException("The number of non-zero " +
                $"elements in the input array should be {expectedCount}.");
        }
    }

    /// <summary>
    /// Determines if the passed infix expression contains consectutive 
    /// operators. Handles the following operators: (,),^,*,/,+,-
    /// </summary>
    /// <param name="infixExp">String representing an infix expression to
    /// evaluate for consecutive operator content.</param>
    /// <returns>A boolean value. True if the passed infix expression string
    /// contains consecutive operators. False otherwise.</returns>
    /// <exception cref="InvalidInputException">Thrown when there is no input
    /// expression or the infix expression contains consecutive operators.
    /// </exception>
    public static void ValidateOperatorOrder(string[] infixExp)
    {
        if (infixExp.Length == 0)
        {
            throw new InvalidInputException("Infix expression is empty.");
        }

        string operators = "()^*/+-";
        for (int i = 0; i < infixExp.Length - 1; i++)
        {
            string tokenA = infixExp[i];
            string tokenB = infixExp[i + 1];

            if ((operators.Contains(tokenA) && operators.Contains(tokenB)))
            {
                // Here, an exception is made. A right paren is allowed to be 
                // adjacent to an operator on its right, and a left paren is 
                // allowed to be adjacent to an operator on its left.
                if (tokenA != ")" && tokenB != "(")
                {
                    throw new InvalidInputException("Infix expression " +
                        $"{String.Join(" ", infixExp)} contains consecutive " +
                        "operators.");
                }
            }
        }
    }

    /// <summary>
    /// Determines if the passed expression contains invalid characters. 
    /// Invalid characters are defined as any character that is neither an 
    /// operator ((,),^,*,/,+,-), decimal point, nor a digit 0-9.
    /// </summary>
    /// <param name="inputs"></param>
    /// <exception cref="InvalidInputException">Thrown when the expression is 
    /// empty or when an invalid token is detected.</exception>
    public static void ValidateInputCharacters(string[] inputs)
    {
        if (inputs.Length == 0)
        {
            throw new InvalidInputException("Expression is empty.");
        }

        string acceptedChars = "0123456789()^*/+-.";

        string expressionString = String.Join("", inputs);
        foreach (char c in expressionString)
        {
            if (!acceptedChars.Contains(c))
            {
                throw new InvalidInputException($"{c} is not a valid token.");
            }
        }
    }
}
