namespace CalculatorLibrary;

/// <summary>
/// Defines the behaviour of a custom operation. An arbitrary number of custom
/// operations may be added to the calculator application.
/// </summary>
public interface ICustomOperation
{
    /// <summary>
    /// Performs a custom operation on the array of inputs and returns a single
    /// value.
    /// </summary>
    /// <param name="inputs">Array of string inputs required to execute the 
    /// custom operation.</param>
    /// <returns>A string value representing the result of the executed 
    /// operation.</returns>
    string Execute(string[] inputs);
}