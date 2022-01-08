using CalculatorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CalculatorLibraryTest;

/// <summary>
/// A collection of unit tests for the CalculatorLibrary.FactorialOperation
/// class.
/// </summary>
[TestClass]
public class FactorialOperationUnitTest
{
    [TestMethod]
    public void Execute_AcceptableInputs_ReturnsExpectedString()
    {
        FactorialOperation operation = new FactorialOperation();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary acceptable input.
            new string[] { "5" },

            // Test 1! behavior.
            new string[] { "1" },

            // Test 0! behavior.
            new string[] { "0" }
        };

        string[] expectedValues = new string[] 
        { 
            "120", 
            "1", 
            "1"
        };

        for (int i = 0; i < inputSets.Count; i++)
        {
            Assert.AreEqual(expectedValues[i], operation.Execute(inputSets[i]));
        }
    }
}