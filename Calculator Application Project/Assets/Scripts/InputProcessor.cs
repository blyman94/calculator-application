using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
    /// <summary>
    /// Operand the user is currently entering.
    /// </summary>
    public string CurrentOperand { get; set; }

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
    /// Text object that displays the state of the clear button based on current
    /// input.
    /// </summary>
    [SerializeField] private TextMeshProUGUI clearAllClearText;

    /// <summary>
    /// Text object that displays the expression the user is currently entering.
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentExpressionText;

    /// <summary>
    /// Text object that displays the operand the user is currently entering.
    /// </summary>
    [SerializeField] private TextMeshProUGUI currentOperandText;

    [SerializeField] private TextMeshProUGUI errorDisplayText;

    /// <summary>
    /// Calculator object to evaluate infix expressions.
    /// </summary>
    private Calculator calculator;

    #region MonoBehaviour Methods
    private void Awake()
    {
        Initialize();
    }
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

        UpdateExpressionText();

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

        UpdateExpressionText();
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
        UpdateClearText();
        UpdateCurrentOperandText();
    }

    /// <summary>
    /// Deletes the most recent character in the current operand.
    /// </summary>
    public void Backspace()
    {
        if (CurrentOperand == "")
        {
            UpdateCurrentOperandText("0");
        }
        else
        {
            CurrentOperand =
                CurrentOperand.Substring(0, CurrentOperand.Length - 1);
            IsResult = false;
            if (CurrentOperand == "")
            {
                UpdateCurrentOperandText("0");
                UpdateClearText();
            }
            else
            {
                UpdateCurrentOperandText();
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
            if (currentOperandText != null && currentOperandText.text != "0")
            {
                UpdateCurrentOperandText("0");
            }
            else
            {
                CurrentExpression.Clear();
                UpdateExpressionText("");
                UnmatchedLeftParenCount = 0;
            }
        }
        else
        {
            CurrentOperand = "";
            UpdateCurrentOperandText("0");
        }
        UpdateClearText();
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

            if (currentExpressionText != null)
            {
                UpdateExpressionText(string.Join(" ", CurrentExpression) + "=");
            }
            CurrentExpression.Clear();

            CurrentOperand = result;
            IsResult = true;

            UpdateCurrentOperandText();
            UpdateErrorText("");
        }
        catch (CalculatorException e)
        {
            UpdateErrorText(e.Message);
            Reset();
        }
    }

    /// <summary>
    /// Initializes the calculator and resets the InputProcessor to an initial 
    /// state.
    /// </summary>
    public void Initialize()
    {
        calculator = new Calculator();
        UpdateErrorText("");
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

            UpdateCurrentOperandText();
        }
    }

    /// <summary>
    /// Resets the InputProcessor to its initial state, where the current 
    /// operand and expression are empty, the current operand is not a result,
    /// and the unmatched parenthesis count is 0.
    /// </summary>
    private void Reset()
    {
        CurrentOperand = "";
        CurrentExpression = new List<string>();
        UpdateCurrentOperandText("0");
        UpdateExpressionText();
        UpdateClearText();
        IsResult = false;
        UnmatchedLeftParenCount = 0;
    }

    /// <summary>
    /// Updates the text on the GUI's clear button to better describe its 
    /// behavior in different scenarios. Pressing the clear button when a 
    /// current operand exists will only clear the operand (referred to as
    /// "clear entry" and represented by "CE"). Pressing the clear button when a
    /// current operand does not exist and an expression exists will clear the
    /// expression (referred to as "clear all" and represented by "C".) If no
    /// operand nor expression exists, the button will default to the clear
    /// behavior.
    /// </summary>
    private void UpdateClearText()
    {
        if (clearAllClearText != null)
        {
            if (CurrentOperand == "")
            {
                clearAllClearText.text = "C";
            }
            else
            {
                clearAllClearText.text = "CE";
            }
        }
    }

    /// <summary>
    /// Updates the GUI to reflect the current operand string.
    /// </summary>
    private void UpdateCurrentOperandText()
    {
        if (currentOperandText != null)
        {
            currentOperandText.text = CurrentOperand;
        }
    }

    /// <summary>
    /// Overload for the UpdateCurrentOpreandText method that accepts a string
    /// and sets the GUI operand text to reflect the passed string.
    /// </summary>
    /// <param name="newText">String to display as current operand in the GUI.
    /// </param>
    private void UpdateCurrentOperandText(string newText)
    {
        if (currentOperandText != null)
        {
            currentOperandText.text = newText;
        }
    }

    /// <summary>
    /// Updates the calculator's error display to reflect the passed message.
    /// </summary>
    /// <param name="errorMessage"></param>
    private void UpdateErrorText(string errorMessage)
    {
        if (errorDisplayText != null)
        {
            errorDisplayText.text = errorMessage;
        }
    }

    /// <summary>
    /// Updates the current expression text to reflect the expression entered.
    /// </summary>
    private void UpdateExpressionText()
    {
        if (currentExpressionText != null)
        {
            currentExpressionText.text = string.Join(" ", CurrentExpression);
        }
    }

    /// <summary>
    /// Overload for the UpdateExpressionText method that accepts a string
    /// and sets the GUI expression text to reflect the passed string.
    /// </summary>
    /// <param name="newText">String to display as current expression.</param>
    private void UpdateExpressionText(string newText)
    {
        if (currentExpressionText != null)
        {
            currentExpressionText.text = newText;
        }
    }
}
