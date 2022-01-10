using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputProcessor : MonoBehaviour
{
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

    /// <summary>
    /// Calculator object to evaluate infix expressions.
    /// </summary>
    private Calculator calculator;

    /// <summary>
    /// Operand the user is currently entering.
    /// </summary>
    private string currentOperand;

    /// <summary>
    /// The expresison the user is currently building for evaluation.
    /// </summary>
    private List<string> currentExpression;

    /// <summary>
    /// Tracks the number of unmatched left parenthesis in the current 
    /// expression.
    /// </summary>
    private int unmatchedLeftParenCount;

    #region MonoBehaviour Methods
    private void Awake()
    {
        calculator = new Calculator();
        currentOperand = "";
        currentExpression = new List<string>();
        unmatchedLeftParenCount = 0;
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
        if (currentOperand != "")
        {
            currentExpression.Add(currentOperand);
        }
        currentExpression.Add(operatorString);
        UpdateExpressionText();

        currentOperand = "";
    }

    /// <summary>
    /// Adds a parenthesis (paren) to the expression based on the expression's
    /// current state.
    /// </summary>
    public void AddParen()
    {
        if (currentExpression.IsEmpty())
        {
            if (currentOperand != "")
            {
                AddOperator("*");
            }

            AddOperator("(");
            unmatchedLeftParenCount++;
        }
        else
        {
            if (currentOperand == "")
            {
                if (currentExpression.Last() == "(")
                {
                    AddOperator("(");
                    unmatchedLeftParenCount++;
                }
                else if (currentExpression.Last() == ")")
                {
                    if (unmatchedLeftParenCount > 0)
                    {
                        AddOperator(")");
                        unmatchedLeftParenCount--;
                    }
                    else
                    {
                        AddOperator("*");
                        AddOperator("(");
                        unmatchedLeftParenCount++;
                    }
                }
                else if (calculator.Operators.Contains(currentExpression.Last()))
                {
                    AddOperator("(");
                    unmatchedLeftParenCount++;
                }
            }
            else
            {
                if (currentExpression.Last() == "(")
                {
                    AddOperator(")");
                    unmatchedLeftParenCount--;
                }
                else if (calculator.Operators.Contains(currentExpression.Last()))
                {
                    AddOperator(")");
                    unmatchedLeftParenCount--;
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
            if (currentOperand == "")
            {
                currentOperand = "0.";
            }
            else
            {
                if (!currentOperand.Contains("."))
                {
                    currentOperand += input;
                }
            }
        }
        else if (input == "0")
        {
            if (currentOperand == "")
            {
                currentOperand = "0";
            }
            else
            {
                if (!(currentOperand == "0"))
                {
                    currentOperand += input;
                }
            }
        }
        else
        {
            if (currentOperand == "0")
            {
                currentOperand = input;
            }
            else
            {
                currentOperand += input;
            }
        }
        UpdateClearText();
        currentOperandText.text = currentOperand;
    }

    /// <summary>
    /// Deletes the most recent character in the current operand.
    /// </summary>
    public void Backspace()
    {
        if (currentOperand == "")
        {
            currentOperandText.text = "0";
        }
        else
        {
            currentOperand =
                currentOperand.Substring(0, currentOperand.Length - 1);
            if (currentOperand == "")
            {
                currentOperandText.text = "0";
                UpdateClearText();
            }
            else
            {
                currentOperandText.text = currentOperand;
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
        if (currentOperand == "")
        {
            if (currentOperandText.text != "0")
            {
                currentOperandText.text = "0";

            }
            else
            {
                currentExpressionText.text = "";
                currentExpression.Clear();
                unmatchedLeftParenCount = 0;
            }
        }
        else
        {
            currentOperand = "";
            currentOperandText.text = "0";
        }
        UpdateClearText();
    }

    /// <summary>
    /// Passes the infix expression built by the user to the calculator. Updates
    /// the display to reflect the calculation.
    /// </summary>
    public void Execute()
    {
        if (currentOperand != "")
        {
            currentExpression.Add(currentOperand);
        }

        string result =
            calculator.AcceptInputArray(currentExpression.ToArray());

        currentExpressionText.text = string.Join(" ", currentExpression) + "=";
        currentExpression.Clear();

        currentOperand = result;
        currentOperandText.text = result;
    }

    /// <summary>
    /// Negates the current operand, allowing the user to toggle between the 
    /// negative and positive representation of that operand.
    /// </summary>
    public void ToggleNegative()
    {
        if (!(currentOperand == ""))
        {
            if (currentOperand.Substring(0, 1) != "-")
            {
                currentOperand = "-" + currentOperand;
            }
            else
            {
                currentOperand = currentOperand.Substring(1, currentOperand.Length);
            }
            currentOperandText.text = currentOperand;
        }
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
        if (currentOperand == "")
        {
            clearAllClearText.text = "C";
        }
        else
        {
            clearAllClearText.text = "CE";
        }
    }

    /// <summary>
    /// Updates the current expression text to reflect the expression entered.
    /// </summary>
    private void UpdateExpressionText()
    {
        currentExpressionText.text = string.Join(" ", currentExpression);
    }
}
