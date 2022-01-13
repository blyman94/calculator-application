using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of unit tests for the PythagoreanSolve class.
/// </summary>
public class PythagoreanSolveTest
{
    [Test]
    public void Execute_AcceptableInputs_ReturnsExpectedString()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

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

    [Test]
    public void Execute_AandBEqualZero_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where a = 0 and b = 0.
            new string[]{ "0", "0", "4" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidInputException>(() =>
                operation.Execute(inputSet));
        }
    }

    [Test]
    public void Execute_AandCEqualZero_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where a = 0 and c = 0.
            new string[]{ "0", "4", "0" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidInputException>(() =>
                operation.Execute(inputSet));
        }
    }

    [Test]
    public void Execute_AllSidesZero_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where a = 0, b = 0, and c = 0.
            new string[]{ "0", "0", "0" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidInputException>(() =>
                operation.Execute(inputSet));
        }
    }

    [Test]
    public void Execute_AGreaterThanC_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where a > c.
            new string[]{ "6", "0", "4" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidTriangleException>(() =>
                operation.Execute(inputSet));
        }
    }

    [Test]
    public void Execute_BandCEqualZero_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where b = 0 and c = 0.
            new string[]{ "4", "0", "0" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidInputException>(() =>
                operation.Execute(inputSet));
        }
    }

    [Test]
    public void Execute_BGreaterThanC_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where b > c.
            new string[]{ "0", "9", "7" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidTriangleException>(() =>
                operation.Execute(inputSet));
        }
    }

    [Test]
    public void Execute_NoSidesZero_ThrowsException()
    {
        PythagoreanSolve operation =
            ScriptableObject.CreateInstance<PythagoreanSolve>();

        List<string[]> inputSets = new List<string[]>()
        {
            // Test arbitrary triangle where a = 0 and c = 0.
            new string[]{ "4", "6", "7" }
        };

        foreach (string[] inputSet in inputSets)
        {
            Assert.Catch<InvalidInputException>(() =>
                operation.Execute(inputSet));
        }
    }
}
