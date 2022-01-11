using System.Collections.Generic;
using NUnit.Framework;

public class CalculatorTest
{
    #region EvaluatePostfixExpression Tests
    [Test]
    public void EvaluatePostfixExpression_AcceptableInput_ReturnsExpectedString()
    {
        Calculator calculator = new Calculator();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test an expression with each of the 5 accepted operators.
            new string[]
            {
                "4.7", "9.1", "*",
                "2", "^","5", "12",
                "/", "+", "100","-"
            }
        };

        string[] expectedValues = new string[]
        {
            "1729.69"
        };

        for (int i = 0; i < inputSets.Count; i++)
        {
            Assert.AreEqual(expectedValues[i],
                calculator.EvaluatePostfixExpression(inputSets[i]));
        }
    }
    #endregion

    #region InfixToPostfix Tests
    [Test]
    public void InfixToPostfix_AcceptableInput_ReturnsExpectedString()
    {
        Calculator calculator = new Calculator();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test infix expression with all 5 accepted operators and parens.
            new string[] { "3", "+", "4", "*", "2","/", "(", "1", "-", "5",")",
                "^", "2", "^", "3" },

            // Test infix expression with all 5 accepted operators and decimals.
            new string[] { "3.1", "+", "4", "*", "2","/", "(", "1", "-", "5.8",
                ")", "^", "2", "^", "3" },

            // Test infix expression that starts with an operator.
            new string[] { "-", "7"},

            // Test infix expression that starts with a parenthesis.
            new string[] {"(", "1", "+", "2", ")"}
        };

        List<string[]> postfixResults = new List<string[]>()
        {
            new string[] { "3", "4", "2", "*", "1", "5", "-", "2", "3", "^",
                "^", "/", "+" },
            new string[] { "3.1", "4", "2", "*", "1", "5.8", "-", "2", "3", "^",
                "^", "/", "+" },
            new string[] {"0", "7", "-"},
            new string[] {"1", "2", "+"}
        };

        for (int i = 0; i < inputSets.Count; i++)
        {
            string expectedString = string.Join(" ", postfixResults[i]);
            string actualString = string.Join(" ", calculator.InfixToPostfix(inputSets[i]));

            Assert.AreEqual(expectedString, actualString);
        }
    }

    [Test]
    public void InfixToPostfix_MismatchedParens_ThrowsException()
    {
        Calculator calculator = new Calculator();

        List<string[]> inputSets = new List<string[]>
        {
            // Test mismatched left parens.
            new string[] { "(", "(", "1", "+", "2", ")", "+", "7" },

            // Test mismatched right parens.
            new string[] { "(", "1", "+", "2", ")", ")", "+", "7" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidExpressionException>(() => 
                calculator.InfixToPostfix(inputSet));
        }
    }
    #endregion

}
