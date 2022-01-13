/// <summary>
/// Defines the behaviour of a an input processor. Input processors are backend
/// engines that allow the user to build expressions and that evaluate those
/// expressions to calculate results.
/// </summary>
public interface IInputProcessor
{
    /// <summary>
    /// Operand the user is currently entering.
    /// </summary>
    string CurrentOperand { get; set; }

    /// <summary>
    /// Delegate to signal an update to the clear button.
    /// </summary>
    UpdateClear UpdateClear { get; set; }

    /// <summary>
    /// Delegate to signal a current operand update.
    /// </summary>
    UpdateCurrentOperand UpdateCurrentOperand { get; set; }

    /// <summary>
    /// Delegate to signal a current expression update.
    /// </summary>
    UpdateCurrentExpression UpdateCurrentExpression { get; set; }

    /// <summary>
    /// Delegate to signal an error update.
    /// </summary>
    UpdateError UpdateError { get; set; }
}
