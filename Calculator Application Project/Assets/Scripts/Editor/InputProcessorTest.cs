using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InputProcessorTests
{
    #region AddOperator Tests
    [Test]
    public void AddOperator_NonEmptyCurrentOperand_OperandAndOperatorAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();

        inputProcessor.CurrentOperand = "5";
        inputProcessor.AddOperator("+");

        Assert.AreEqual("5 +",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddOperator_EmptyCurrentOperand_OperatorAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();

        inputProcessor.AddOperator("+");

        Assert.AreEqual("+",
            string.Join(" ", inputProcessor.CurrentExpression));
    }
    #endregion

    #region AddParen Tests
    [Test]
    public void AddParen_ExpressionEmptyAndCurrentOperandEmpty_LeftParenAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.AddParen();

        Assert.AreEqual("(",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEmptyAndCurrentOperandNotEmpty_ExplicitMultiplicationAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5";
        inputProcessor.AddParen();

        Assert.AreEqual("5 * (",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithLeftParenAndCurrentOperandEmpty_LeftParenAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentExpression = new List<string> { "(" };
        inputProcessor.AddParen();

        Assert.AreEqual("( (",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithLeftParenAndCurrentOperandNotEmpty_OperandAndRightParenAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5";
        inputProcessor.CurrentExpression = new List<string> { "(" };
        inputProcessor.AddParen();

        Assert.AreEqual("( 5 )",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithOperatorAndCurrentOperandEmpty_LeftParenAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentExpression = new List<string> { "5", "+" };
        inputProcessor.AddParen();

        Assert.AreEqual("5 + (",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithOperatorAndCurrentOperandNotEmpty_OperandAndRightParenAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5";
        inputProcessor.CurrentExpression = new List<string> { "(", "1", "+" };
        inputProcessor.AddParen();

        Assert.AreEqual("( 1 + 5 )",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithRightParenAndCurrentOperandEmptyAndMatchedParens_ExplicitMultiplicationAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentExpression = new List<string> { "(", "(", "5", "+", "1", ")", ")" };
        inputProcessor.AddParen();

        Assert.AreEqual("( ( 5 + 1 ) ) * (",
            string.Join(" ", inputProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithRightParenAndCurrentOperandEmptyAndUnmatchedParens_RightParenAdded()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        inputProcessor.Initialize();
        inputProcessor.CurrentExpression = new List<string> { "(", "(", "5", "+", "1", ")" };

        // Since the expression is given instead of construction, artifically
        // add the unmatched left paren count.
        inputProcessor.UnmatchedLeftParenCount = 1;

        inputProcessor.AddParen();

        Assert.AreEqual("( ( 5 + 1 ) )",
            string.Join(" ", inputProcessor.CurrentExpression));
    }
    #endregion

    #region AddToCurrentInput Tests
    [Test]
    public void AddToCurrentInput_InputIsDecimalAndCurrentOperandEmpty_AddsZeroPlusDecimal()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.AddToCurrentInput(".");
        Assert.AreEqual("0.", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndCurrentOperandNotEmptyAndCurrentOperandDecimal_DoesNotAddDecimal()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5.";
        inputProcessor.AddToCurrentInput(".");
        Assert.AreEqual("5.", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndCurrentOperandNotEmptyAndCurrentOperandNoDecimal_AddsDecimal()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5";
        inputProcessor.AddToCurrentInput(".");
        Assert.AreEqual("5.", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandNotEmptyAndCurrentOperandNotZero_AddInput()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "0";
        inputProcessor.AddToCurrentInput("5");
        Assert.AreEqual("5", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandNotEmptyAndCurrentOperandZero_ChangeToInput()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "0";
        inputProcessor.AddToCurrentInput("5");
        Assert.AreEqual("5", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandEmpty_CurrentOperandIsZero()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.AddToCurrentInput("0");
        Assert.AreEqual("0", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandNotEmptyAndCurrentOperandNotZero_AddsZero()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5";
        inputProcessor.AddToCurrentInput("0");
        Assert.AreEqual("50", inputProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandNotEmptyAndCurrentOperandZero_DoesNotAddZero()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "0";
        inputProcessor.AddToCurrentInput("0");
        Assert.AreEqual("0", inputProcessor.CurrentOperand);
    }
    #endregion

    #region Backspace Tests
    [Test]
    public void Backspace_CurrentOperandNotEmpty_RemovesLastToken()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "506";
        inputProcessor.Backspace();
        Assert.AreEqual("50", inputProcessor.CurrentOperand);
    }
    #endregion

    #region ClearInput Tests
    [Test]
    public void ClearInput_CurrentOperandEmpty_ClearsExpression()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentExpression = new List<string>() { "1", "+", "2" };
        inputProcessor.ClearInput();
        Assert.AreEqual(0, inputProcessor.CurrentExpression.Count);
    }

    [Test]
    public void ClearInput_CurrentOperandNotEmpty_ClearsOperand()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "500";
        inputProcessor.ClearInput();
        Assert.AreEqual("", inputProcessor.CurrentOperand);
    }
    #endregion

    #region Execute Tests
    [Test]
    public void Execute_ComplexExpressions_ResultIsCorrect()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();

        List<List<string>> infixExpressions = new List<List<string>>()
        {
            // Handles decimal values
            new List<string>(){"6.23", "+", "0.019", "*", "(", "23.41", "^",
                "2.03", ")"},
            // Handles negative decimal values
            new List<string>(){"-6.23", "+", "(", "-0.019", ")", "*", "(", "23.41", "^",
                "-2.03", ")"},
            // Handles all operators and nested parenthesis
            new List<string>(){"(", "-5", "+", "(", "(", "5", "^", "2", ")",
                "-", "(", "4", "*", "3", "*", "-7", ")", ")", "^", "(", "1",
                "/", "2", ")", ")", "/", "(", "2", "*", "3", ")"},
        };

        string[] expectedResults = new string[]
        {
            "17.6756","-6.230031","0.9067178"
        };

        for (int i = 0; i < infixExpressions.Count; i++)
        {
            inputProcessor.Initialize();
            inputProcessor.CurrentExpression = infixExpressions[i];
            inputProcessor.Execute();
            Assert.AreEqual(expectedResults[i], inputProcessor.CurrentOperand);
        }

    }
    #endregion

    #region ToggleNegative Tests
    [Test]
    public void ToggleNegative_CurrentOperandNotEmptyAndCurrentOperandNegative_OperandPositive()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "-5";
        inputProcessor.ToggleNegative();
        Assert.AreEqual("5", inputProcessor.CurrentOperand);
    }

    [Test]
    public void ToggleNegative_CurrentOperandNotEmptyAndCurrentOperandNotNegative_NegatesOperand()
    {
        GameObject go = new GameObject();
        InputProcessor inputProcessor = go.AddComponent<InputProcessor>();
        inputProcessor.Initialize();
        inputProcessor.CurrentOperand = "5";
        inputProcessor.ToggleNegative();
        Assert.AreEqual("-5", inputProcessor.CurrentOperand);
    }    
    #endregion

}
