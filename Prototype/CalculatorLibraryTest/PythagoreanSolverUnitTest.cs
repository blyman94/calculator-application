using CalculatorLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CalculatorLibraryTest;

/// <summary>
/// A collection of unit tests for the CalculatorLibrary.PythagoreanSolver
/// class.
/// </summary>
[TestClass]
public class PythagoreanSolverUnitTest
{
    [TestMethod]
    public void Execute_AcceptableInputs_ReturnsExpectedString()
    {
        PythagoreanSolver operation = new PythagoreanSolver();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary, acceptable triangle where b = 0.
            new string[] { "5", "0", "13" },

            // Test arbitrary, acceptable triangle where c = 0.
            new string[] { "6", "8", "0" },

            // Test arbitrary triangle where a = 0.
            new string[] { "0", "15", "17" }
        };

        string[] expectedValues = new string[] 
        { 
            "12", 
            "10", 
            "8"
        };

        for (int i = 0; i < inputSets.Count; i++)
        {
            Assert.AreEqual(expectedValues[i], operation.Execute(inputSets[i]));
        }
    }

    [TestMethod]
    public void Execute_AGreaterThanC_ThrowsException()
    {
        PythagoreanSolver operation = new PythagoreanSolver();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where a > c.
            new string[]{ "6", "0", "4" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.ThrowsException<InvalidTriangleException>(() =>
                operation.Execute(inputSet));
        }
    }

    [TestMethod]
    public void Execute_BGreaterThanC_ThrowsException()
    {
        PythagoreanSolver operation = new PythagoreanSolver();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where b > c.
            new string[]{ "0", "9", "7" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.ThrowsException<InvalidTriangleException>(() =>
                operation.Execute(inputSet));
        }
    }
}