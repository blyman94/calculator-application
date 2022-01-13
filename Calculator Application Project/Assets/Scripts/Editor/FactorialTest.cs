using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of unit tests for the Factorial class.
/// </summary>
public class FactorialTest
{
    [Test]
    public void Execute_AcceptableInputs_ReturnsExpectedString()
    {
        Factorial operation = ScriptableObject.CreateInstance<Factorial>();

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
