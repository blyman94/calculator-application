/// <summary>
/// Delegate used to signal that the current operand has been updated by the
/// backend.
/// </summary>
/// <param name="currentOperandString">String representing the current operand 
/// in the backend calculator, which will be displayed as the current operand in
/// the frontend calculator display.</param>
public delegate void UpdateCurrentOperand(string currentOperandString);
