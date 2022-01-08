using CalculatorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CalculatorLibraryTest;

/// <summary>
/// A collection of unit tests for the CalculatorLibrary.PythagoreanSolver
/// class.
/// </summary>
[TestClass]
public class MetricConverterUnitTest
{
    [TestMethod]
    public void Execute_AcceptableInputs_ReturnsExpectedString()
    {
        MetricConverter operation = new MetricConverter();

        List<string[]> inputSets = new List<string[]>
        {
            // Test arbitrary non-integer value.
            new string[] { "5.25" },

            // Test arbitrary integer value.
            new string[] { "4" }
        };

        string[] expectedValues = new string[] 
        { 
            "13.335", 
            "10.16" 
        };

        for (int i = 0; i < inputSets.Count; i++)
        {
            Assert.AreEqual(expectedValues[i], operation.Execute(inputSets[i]));
        }
    }
}