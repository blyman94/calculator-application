namespace CalculatorLibrary;

/// <summary>
/// Responsible for accepting a single input representing a length in inches and
/// returning the length converted to centimeters.
/// </summary>
public class MetricConverter : ICustomOperation
{
    /// <summary>
    /// Parameterless constructor for the MetricConverter class. 
    /// </summary>
    public MetricConverter()
    {

    }

    public string Execute(string[] inputs)
    {
        InputValidator.ValidateInputCount(inputs, 1);
        InputValidator.ValidateInputSign(inputs, false);

        float impLength = float.Parse(inputs[0]);
        return (impLength * 2.54f).ToString();
    }
}