/// <summary>
/// Defines the behaviour of a custom operation. An arbitrary number of custom
/// operations may be added to the calculator application.
/// </summary>
public interface ICustomOperation
{
    /// <summary>
    /// Can this custom operation accept decimal values (non-integers)?
    /// </summary>
    bool AllowsDecimal { get; }
    
    /// <summary>
    /// Can this custom operation accept negative values?
    /// </summary>
    bool AllowsNegative { get; }

    /// <summary>
    /// Returns an array of strings representing the labels of all inputs to the
    /// custom operation.
    /// </summary>
    string[] ArgumentLabels { get; }

    /// <summary>
    /// Returns the description of this custom operation as a string.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Performs a custom operation on the array of inputs and returns a single
    /// value.
    /// </summary>
    /// <param name="inputs">Array of string inputs required to execute the 
    /// custom operation.</param>
    /// <returns>A string value representing the result of the executed 
    /// operation.</returns>
    string Execute(string[] inputs);

    /// <summary>
    /// Returns the instructions for this custom operation as a string.
    /// </summary>
    string Instructions { get; }

    /// <summary>
    /// Returns the name of this custom operation as a string.
    /// </summary>
    string Name { get; }
}