using UnityEngine;

/// <summary>
/// Responsible for accepting a single input representing a length in inches and
/// returning the length converted to centimeters.
/// </summary>
[CreateAssetMenu(menuName = "Custom Operation.../Metric Converter",
    fileName = "MetricConverter")]
public class MetricConverter : CustomOperation
{
    public override string Execute(string[] inputs)
    {
        float impLength = float.Parse(inputs[0]);
        return (impLength * 2.54f).ToString();
    }
}
