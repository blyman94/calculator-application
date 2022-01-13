using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Collection of unit tests for the MetricConverter class.
/// </summary>
public class MetricConverterTest
{
    [Test]
    public void Execute_AcceptableInputs_ReturnsExpectedString()
    {
        MetricConverter operation = 
            ScriptableObject.CreateInstance<MetricConverter>();

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