/// <summary>
/// Delegate used to signal that the current expression has been updated by the
/// backend.
/// </summary>
/// <param name="currentExpressionString">String representing the current 
/// expression in the backend calculator, which will be displayed as the current 
/// expression in the frontend calculator display.</param>
public delegate void UpdateCurrentExpression(string currentExpressionString);