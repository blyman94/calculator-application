using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for performing primitive operations or custom operations on 
/// received input and displaying results in the GUI. It will also store the 
/// current value as an operand to be used in future calculations. 
/// </summary>
public class Calculator
{
    /// <summary>
    /// Value currently displayed on the calculator. Can be used as the first
    /// argument to an infix expression evaluation.
    /// </summary>
    public float CurrentValue;

    /// <summary>
    /// String containing all accepted primitive operators for infix expression 
    /// evaluation.
    /// </summary>
    public string Operators = "^*/+-()";

    // Operator Precedence and Associativity

    /// <summary>
    /// Dictionary to retrieve precedence value of an operator represented as a
    /// string.
    /// </summary>
    private Dictionary<string, int> precedence;

    /// <summary>
    /// Dictionary to retrieve the associativity behavior of an operator 
    /// represented as a string.
    /// </summary>
    private Dictionary<string, bool> leftAssociative;

    /// <summary>
    /// Parameterless constructor for the Calculator class. Initializes the 
    /// current value of the calculator to 0 and the initializes the precedence 
    /// and associativity dictionaries used for expression evaluation.
    /// Initializes all available custom operation classes.
    /// </summary>
    public Calculator()
    {
        CurrentValue = 0;
        AssignPrecedenceValues();
        AssignLeftAssociativeValues();
    }

    /// <summary>
    /// Reads a string array and determines whether to execute a custom
    /// operation on the array, clear the current value, or to treat the 
    /// incoming array as an infix expression and evaluate it.
    /// </summary>
    /// <param name="inputArray">Array of inputs to be evaluated.</param>
    /// <returns>String representing the resulting value of the selected 
    /// operation or infix expression evaluation.</returns>
    public string AcceptInputArray(string[] inputArray)
    {
        string result = EvaluatePostfixExpression(InfixToPostfix(inputArray));
        CurrentValue = float.Parse(result);
        return result;
    }

    /// <summary>
    /// Evaluates a postfix expression to calculate a resulting value.
    /// </summary>
    /// <param name="postfixExpression">Postfix expression to evaluate.</param>
    /// <returns>A string value representing the result of the postfix 
    /// expression.</returns>
    public string EvaluatePostfixExpression(string[] postfixExpression)
    {
        Stack<float> operandStack = new Stack<float>();

        foreach (string token in postfixExpression)
        {
            bool isOperand = float.TryParse(token, out float operand);
            bool isOperator = Operators.Contains(token);

            if (isOperand)
            {
                operandStack.Push(operand);
            }
            else
            {
                float rightOperand = operandStack.Pop();
                float leftOperand = operandStack.Pop();

                float operationResult;
                switch (token)
                {
                    case "^":
                        operationResult = Mathf.Pow(leftOperand, rightOperand);
                        break;
                    case "*":
                        operationResult = leftOperand * rightOperand;
                        break;
                    case "/":
                        operationResult = leftOperand / rightOperand;
                        break;
                    case "+":
                        operationResult = leftOperand + rightOperand;
                        break;
                    case "-":
                        operationResult = leftOperand - rightOperand;
                        break;
                    default:
                        // The default condition should never occur, as inputs
                        // are validated (either implicitly or explicitly) prior 
                        // to postfix evaluation.
                        operationResult = 0.0f;
                        break;
                }
                operandStack.Push(operationResult);
            }
        }
        CurrentValue = operandStack.Pop();
        return CurrentValue.ToString();
    }

    /// <summary>
    /// Uses the shunting-yard algorithm to convert a string array representing 
    /// an infix expression passed as a parameter to a string array representing 
    /// a postfix expression. Although infix expressions are more readable for 
    /// humans, postfix expressions have more objective evaluation procedures 
    /// and are more readable by programs.
    /// </summary>
    /// <param name="infixExpression">Infix expression to convert to a 
    /// postfix representation.</param>
    /// <returns>String array representing the postfix representation of the 
    /// infix expression passed as a parameter.</returns>
    public string[] InfixToPostfix(string[] infixExpression)
    {
        Stack<string> operatorStack = new Stack<string>();
        List<string> resultList = new List<string>();

        for (int i = 0; i < infixExpression.Length; i++)
        {
            string token = infixExpression[i];
            bool isOperand = float.TryParse(token, out float operand);
            bool isOperator = Operators.Contains(token);

            if (isOperand)
            {
                resultList.Add(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                while (operatorStack.Count > 0 && operatorStack.Peek() != "(")
                {
                    resultList.Add(operatorStack.Pop());
                }

                if (operatorStack.IsEmpty())
                {
                    throw new InvalidExpressionException("Syntax Error: " + 
                        "Infix expression has mismatched parentheses.");
                }

                if (operatorStack.Peek() == "(")
                {
                    operatorStack.Pop();
                }
            }
            else
            {
                // If the first token is ^,*,/,+, or -, use the calculator's
                // current value as the first operand before handling the
                // operator.
                if (i == 0)
                {
                    resultList.Add(CurrentValue.ToString());
                }

                while (!operatorStack.IsEmpty() &&
                    (precedence[operatorStack.Peek()] > precedence[token] ||
                    (precedence[operatorStack.Peek()] == precedence[token] &&
                    leftAssociative[token])))
                {
                    resultList.Add(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
        }

        while (!operatorStack.IsEmpty())
        {
            if (operatorStack.Peek() == "(" || operatorStack.Peek() == ")")
            {
                throw new InvalidExpressionException("Syntax Error: Infix " + 
                    "expression has mismatched parentheses.");
            }
            resultList.Add(operatorStack.Pop());
        }

        return resultList.ToArray();
    }

    /// <summary>
    /// Assigns associativity values to each operator and stores the key-value
    /// pair in the leftAssociative dictionary.
    /// </summary>
    private void AssignLeftAssociativeValues()
    {
        leftAssociative = new Dictionary<string, bool>();
        
        leftAssociative.Add("^", false);
        leftAssociative.Add("*", true);
        leftAssociative.Add("/", true);
        leftAssociative.Add("+", true);
        leftAssociative.Add("-", true);
        leftAssociative.Add("(", true);
    }

    /// <summary>
    /// Assigns precedence values to each operator and stores the key-value
    /// pair in the precedence dictionary.
    /// </summary>
    private void AssignPrecedenceValues()
    {
        precedence = new Dictionary<string, int>();

        precedence.Add("^", 4);
        precedence.Add("*", 3);
        precedence.Add("/", 3);
        precedence.Add("+", 2);
        precedence.Add("-", 2);
        precedence.Add("(", 1);
    }
}
