namespace CalculatorLibrary;

/// <summary>
/// A collection of extension methods used for the calculator application. 
/// </summary>
public static class ExtensionMethods
{
    #region Stack Extension Methods
    /// <summary>
    /// Determines if the stack is empty based on its count of elements.
    /// </summary>
    /// <typeparam name="T">Generic type of the stack.</typeparam>
    /// <param name="stack">Stack to test for emptiness</param>
    /// <returns>Boolean value. True if the stack does not contain any elements,
    /// false otherwise.</returns>
    public static bool IsEmpty<T>(this Stack<T> stack)
    {
        return stack.Count <= 0;
    }
    #endregion
}