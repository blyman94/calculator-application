using System.Collections.Generic;

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

    #region List Extension Methods
    /// <summary>
    /// Determines if the list is empty based on its count of elements.
    /// </summary>
    /// <typeparam name="T">Generic type of the list.</typeparam>
    /// <param name="list">List to test for emptiness</param>
    /// <returns>Boolean value. True if the list does not contain any elements,
    /// false otherwise.</returns>
    public static bool IsEmpty<T>(this List<T> list)
    {
        return list.Count <= 0;
    }
    #endregion
}