using System.Linq;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receives input from the calculator GUI to use in infix expression evaluation.
/// </summary>
public class InfixExpressionProcessor : MonoBehaviour, IInputProcessor
{
    /// <summary>
    /// True if the calculator has already been cleared once.
    /// </summary>
    public bool ClearedOnce { get; set; }

    /// <summary>
    /// The expresison the user is currently building for evaluation.
    /// </summary>
    public List<string> CurrentExpression { get; set; }

    /// <summary>
    /// Determines if the current operand is the result of an operation.
    /// </summary>
    public bool IsResult { get; set; }

    /// <summary>
    /// Tracks the number of unmatched left parenthesis in the current 
    /// expression.
    /// </summary>
    public int UnmatchedLeftParenCount { get; set; }

    /// <summary>
    /// Calculator object to evaluate infix expressions.
    /// </summary>
    private Calculator calculator;

    #region MonoBehaviour Methods
    private void Start()
    {
        Initialize();
    }
    #endregion

    #region IInputProcessor Methods
    public string CurrentOperand { get; set; }

    public UpdateClear UpdateClear { get; set; }

    public UpdateCurrentOperand UpdateCurrentOperand { get; set; }

    public UpdateCurrentExpression UpdateCurrentExpression { get; set; }

    public UpdateError UpdateError { get; set; }
    #endregion

    /// <summary>
    /// Updates the currentExpression list with the currentOperand and the
    /// passed operator. Updates currentExpressionText to reflect the updated 
    /// expression.
    /// </summary>
    /// <param name="operatorString">String representing the operator to be 
    /// added to the current expression.</param>
    public void AddOperator(string operatorString)
    {
        if (CurrentOperand != "")
        {
            CurrentExpression.Add(CurrentOperand);
        }
        CurrentExpression.Add(operatorString);

        UpdateCurrentExpression?.Invoke(string.Join(" ", CurrentExpression));

        CurrentOperand = "";
    }

    /// <summary>
    /// Adds a parenthesis (paren) to the expression based on the expression's
    /// current state.
    /// </summary>
    public void AddParen()
    {
        if (CurrentExpression.IsEmpty())
        {
            if (CurrentOperand != "")
            {
                AddOperator("*");
            }

            AddOperator("(");
            UnmatchedLeftParenCount++;
        }
        else
        {
            if (CurrentOperand == "")
            {
                if (CurrentExpression.Last() == "(")
                {
                    AddOperator("(");
                    UnmatchedLeftParenCount++;
                }
                else if (CurrentExpression.Last() == ")")
                {
                    if (UnmatchedLeftParenCount > 0)
                    {
                        AddOperator(")");
                        UnmatchedLeftParenCount--;
                    }
                    else
                    {
                        AddOperator("*");
                        AddOperator("(");
                        UnmatchedLeftParenCount++;
                    }
                }
                else if (calculator.Operators.Contains(CurrentExpression.Last()))
                {
                    AddOperator("(");
                    UnmatchedLeftParenCount++;
                }
            }
            else
            {
                if (CurrentExpression.Last() == "(")
                {
                    AddOperator(")");
                    UnmatchedLeftParenCount--;
                }
                else if (calculator.Operators.Contains(CurrentExpression.Last()))
                {
                    AddOperator("*");
                    AddOperator("(");
                    UnmatchedLeftParenCount++;
                }
            }
        }

        UpdateCurrentExpression?.Invoke(string.Join(" ", CurrentExpression));
    }

    /// <summary>
    /// Adds a new input string to the user's current input.
    /// </summary>
    /// <param name="input">String to be added to the user's current 
    /// input.</param>
    public void AddToCurrentInput(string input)
    {
        if (input == ".")
        {
            if (CurrentOperand == "")
            {
                CurrentOperand = "0.";
            }
            else
            {
                if (!CurrentOperand.Contains("."))
                {
                    CurrentOperand += input;
                }
            }
        }
        else if (input == "0")
        {
            if (CurrentOperand == "")
            {
                CurrentOperand = "0";
            }
            else
            {
                if (!(CurrentOperand == "0"))
                {
                    CurrentOperand += input;
                }
            }
        }
        else
        {
            if (CurrentOperand == "0" || IsResult)
            {
                CurrentOperand = input;
            }
            else
            {
                CurrentOperand += input;
            }
        }

        IsResult = false;

        UpdateClear?.Invoke(CurrentOperand);
        UpdateCurrentOperand?.Invoke(CurrentOperand);
    }

    /// <summary>
    /// Deletes the most recent character in the current operand.
    /// </summary>
    public void Backspace()
    {
        if (CurrentOperand == "")
        {
            UpdateCurrentOperand?.Invoke("0");
        }
        else
        {
            CurrentOperand =
                CurrentOperand.Substring(0, CurrentOperand.Length - 1);
            IsResult = false;
            if (CurrentOperand == "")
            {
                UpdateCurrentOperand?.Invoke("0");
                UpdateClear?.Invoke(CurrentOperand);
            }
            else
            {
                UpdateCurrentOperand?.Invoke(CurrentOperand);
            }
        }
    }

    /// <summary>
    /// If an operand exists, clears the operand and sets the calculator's 
    /// display to read "O." If an operand does not exist, clears the current
    /// expression.
    /// </summary>
    public void ClearInput()
    {
        if (CurrentOperand == "")
        {
            if (!ClearedOnce)
            {
                UpdateCurrentOperand?.Invoke("0");
                ClearedOnce = true;
            }
            else
            {
                CurrentExpression.Clear();
                UpdateCurrentExpression?.Invoke("");
                UnmatchedLeftParenCount = 0;
                ClearedOnce = false;
            }
        }
        else
        {
            CurrentOperand = "";
            UpdateCurrentOperand?.Invoke("0");
            ClearedOnce = true;
        }

        UpdateClear?.Invoke(CurrentOperand);
    }

    /// <summary>
    /// Passes the infix expression built by the user to the calculator. Updates
    /// the display to reflect the calculation.
    /// </summary>
    public void Execute()
    {
        try
        {
            if (CurrentOperand != "")
            {
                CurrentExpression.Add(CurrentOperand);
            }

            string result =
                calculator.AcceptInputArray(CurrentExpression.ToArray());

            UpdateCurrentExpression?.Invoke(string.Join(" ",
                CurrentExpression) + "=");
            CurrentExpression.Clear();

            CurrentOperand = result;
            IsResult = true;

            UpdateCurrentOperand?.Invoke(CurrentOperand);
            UpdateError?.Invoke("");
        }
        catch (CalculatorException e)
        {
            UpdateError?.Invoke(e.Message);
            Reset();
        }
    }

    /// <summary>
    /// Initializes the calculator and resets the InfixExpressionProcessor to an 
    /// initial state.
    /// </summary>
    public void Initialize()
    {
        calculator = new Calculator();
        UpdateError?.Invoke("");
        Reset();
    }

    /// <summary>
    /// Negates the current operand, allowing the user to toggle between the 
    /// negative and positive representation of that operand.
    /// </summary>
    public void ToggleNegative()
    {
        if (!(CurrentOperand == ""))
        {
            if (CurrentOperand.Substring(0, 1) != "-")
            {
                CurrentOperand = "-" + CurrentOperand;
            }
            else
            {
                CurrentOperand =
                    CurrentOperand.Substring(1, CurrentOperand.Length - 1);
            }

            if (IsResult)
            {
                IsResult = false;
            }

            UpdateCurrentOperand?.Invoke(CurrentOperand);
        }
    }

    /// <summary>
    /// Resets the InfixExpressionProcessor to its initial state, where the 
    /// current operand and expression are empty, the current operand is not a 
    /// result, and the unmatched parenthesis count is 0.
    /// </summary>
    private void Reset()
    {
        ClearedOnce = false;
        CurrentOperand = "";
        CurrentExpression = new List<string>();
        UpdateCurrentOperand?.Invoke("0");
        UpdateCurrentExpression?.Invoke("");
        UpdateClear?.Invoke(CurrentOperand);
        IsResult = false;
        UnmatchedLeftParenCount = 0;
    }
}
