using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorLibrary;
using System.Collections.Generic;

namespace CalculatorLibraryTest;

/// <summary>
/// A collection of unit tests for the CalculatorLibrary.InputValidator class.
/// </summary>
[TestClass]
public class InputValidatorUnitTest
{
    [TestMethod]
    public void CompareToZero_ValueIsGreaterThanZero_ReturnsTrue()
    {
        string[] inputValues = new string[]
        {
            // Test arbitrary value greater than 0.
            "1.05"
        };
        foreach (string value in inputValues)
        {
            Assert.IsTrue(InputValidator.CompareToZero(value, false));
        }
    }

    [TestMethod]
    public void CompareToZero_ValueIsLessThanZero_ReturnsFalse()
    {
        string[] inputValues = new string[]
        {
            // Test arbitrary value less than zero.
            "-1.05"
        };
        foreach (string value in inputValues)
        {
            Assert.IsFalse(InputValidator.CompareToZero(value, false));
        }
    }

    [TestMethod]
    public void CompareToZero_ValueIsEqualToZeroAndOrEqualToFalse_ReturnsFalse()
    {
        // Test 0.
        Assert.IsFalse(InputValidator.CompareToZero("0", false));
    }

    [TestMethod]
    public void CompareToZero_ValueIsEqualToZeroAndOrEqualToTrue_ReturnsTrue()
    {
        // Test 0.
        Assert.IsTrue(InputValidator.CompareToZero("0", true));
    }

    [TestMethod]
    public void ContainsLeadingZeros_DoesContainLeadingZeroes_ReturnsTrue()
    {
        string[] inputValues = new string[]
        {
            // Test arbitrary value w/ leading zeros and no decimal point.
            "005",

            // Test arbitrary value w/ leading zeros and a decimal point.
            "00.516",
        };
        foreach (string value in inputValues)
        {
            Assert.IsTrue(InputValidator.ContainsLeadingZeros(value));
        }
    }

    [TestMethod]
    public void ContainsLeadingZeros_DoesNotContainLeadingZeroes_ReturnsFalse()
    {
        string[] inputValues = new string[]
        {
            // Test arbitrary value w/o leading zeros and no decimal point.
            "15",

            // Test arbitrary value w/o leading zeros and a decimal point.
            "2.01",
        };
        foreach (string value in inputValues)
        {
            Assert.IsFalse(InputValidator.ContainsLeadingZeros(value));
        }
    }

    [TestMethod]
    public void ValidateInputCharacters_AllValidCharacters_DoesNotThrowException()
    {
        List<string[]> infixExpressions = new List<string[]>()
        {
            // Test arbitrary expression with all valid characters.
            new string[]{ "6", "+", "(", "-", "+", "5", ")","*","7" },
        };

        foreach (string[] inputSet in infixExpressions)
        {
            InputValidator.ValidateInputCharacters(inputSet);
        }
    }

    [TestMethod]
    public void ValidateInputCharacters_InvalidCharacters_ThrowsException()
    {
        List<string[]> infixExpressions = new List<string[]>()
        {
            // Test arbitrary expression with an invalid character.
            new string[]{ "3", "&", "-" }
        };

        foreach (string[] inputSet in infixExpressions)
        {
            Assert.ThrowsException<InvalidInputException>(() =>
                InputValidator.ValidateInputCharacters(inputSet));
        }
    }

    [TestMethod]
    public void ValidateInputCount_CountIsExpected_DoesNotThrowException()
    {
        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary set with expected count of 3.
            new string[]{ "3", "0", "1" }
        };

        foreach (string[] inputSet in inputSets)
        {
            InputValidator.ValidateInputCount(inputSet, 3);
        }
    }

    [TestMethod]
    public void ValidateInputCount_CountIsNotExpected_ThrowsException()
    {
        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary set with an unexpected count of 3 (should be 2).
            new string[]{ "3", "0", "1" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.ThrowsException<InvalidInputException>(() =>
                InputValidator.ValidateInputCount(inputSet,2));
        }
    }

    [TestMethod]
    public void ValidateInputInteger_IsAnInteger_DoesNotThrowException()
    {
        string[] inputValues = new string[]
        {
            // Test arbitrary integer.
            "3",

            // Test 0 behavior.
            "0",
        };

        foreach (string input in inputValues)
        {
            InputValidator.ValidateInputInteger(input);
        }
    }

    [TestMethod]
    public void ValidateInputInteger_IsNotAnInteger_ThrowsException()
    {
        string[] inputValues = new string[]
        {
            // Test arbitrary non-integer value.
            "3.14",
        };

        foreach (string input in inputValues)
        {
            Assert.ThrowsException<InvalidInputException>(() =>
                InputValidator.ValidateInputInteger(input));
        }
    }

    [TestMethod]
    public void ValidateInputSign_AllowsZeroAndHasZero_DoesNotThrowException()
    {
        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary set that has 0.
            new string[]{ "3", "0", "1" }
        };

        foreach (string[] inputSet in inputSets)
        {
            InputValidator.ValidateInputSign(inputSet, true);
        }
    }

    [TestMethod]
    public void ValidateInputSign_DoesNotAllowZeroAndHasZero_ThrowsException()
    {
        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary set that has 0.
            new string[]{ "3", "0", "1" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.ThrowsException<InvalidInputException>(() =>
                InputValidator.ValidateInputSign(inputSet, false));
        }
    }

    [TestMethod]
    public void ValidateInputSign_HasNegativeNonZeroLengths_ThrowsException()
    {
        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary set with non-zero negative length.
            new string[]{ "-3", "0", "1" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.ThrowsException<InvalidInputException>(() =>
                InputValidator.ValidateInputSign(inputSet, true));
        }
    }

    [TestMethod]
    public void ValidateInputSign_HasNoNegativeNonZeroLengths_DoesNotThrowException()
    {
        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary set with no non-zero negative length.
            new string[]{ "3", "5", "1" }
        };

        foreach (string[] inputSet in inputSets)
        {
            InputValidator.ValidateInputSign(inputSet, true);
        }
    }

    [TestMethod]
    public void ValidateOperatorOrder_ContainsConsecutiveOperators_ThrowsException()
    {
        List<string[]> infixExpressions = new List<string[]>()
        {
            // Test arbitrary expression with consecutive operators.
            new string[]{ "6", "+", "(", "-", "+", "5", ")","*","7" },
        };

        foreach (string[] expression in infixExpressions)
        {
            Assert.ThrowsException<InvalidInputException>(() =>
                InputValidator.ValidateOperatorOrder(expression));
        }
    }

    [TestMethod]
    public void ValidateOperatorOrder_DoesNotContainConsecutiveOperators_DoesNotThrowException()
    {
        List<string[]> infixExpressions = new List<string[]>()
        {
            // Test arbitrary expression without consecutive operators.
            new string[]{ "6", "+", "(", "4", "+", "5", ")","*","7" },
        };
        foreach (string[] expression in infixExpressions)
        {
            InputValidator.ValidateOperatorOrder(expression);
        }
    }
}