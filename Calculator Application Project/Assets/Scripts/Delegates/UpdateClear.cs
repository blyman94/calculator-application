/// <summary>
/// Delegate used to signal that the clear/clear entry button should be updated
/// to reflect the current state of the calculator.
/// </summary>
/// <param name="currentOperandString">String representing the current operand
/// of the calculator, to assist in determining the proper string to display
/// on the clear/clear entry button.</param>
public delegate void UpdateClear(string currentOperandString);