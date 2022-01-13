using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Collection of unit tests for the InfixExpressionProcessor class.
/// </summary>
public class InfixExpressionProcessorTests
{
    #region AddOperator Tests
    [Test]
    public void AddOperator_EmptyCurrentOperand_OperatorAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();

        infExpProcessor.AddOperator("+");

        Assert.AreEqual("+",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }
    
    [Test]
    public void AddOperator_NonEmptyCurrentOperand_OperandAndOperatorAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();

        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.AddOperator("+");

        Assert.AreEqual("5 +",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }
    #endregion

    #region AddParen Tests
    [Test]
    public void AddParen_ExpressionEmptyAndCurrentOperandEmpty_LeftParenAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.AddParen();

        Assert.AreEqual("(",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEmptyAndCurrentOperandNotEmpty_ExplicitMultiplicationAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.AddParen();

        Assert.AreEqual("5 * (",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithLeftParenAndCurrentOperandEmpty_LeftParenAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentExpression = new List<string> { "(" };
        infExpProcessor.AddParen();

        Assert.AreEqual("( (",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithLeftParenAndCurrentOperandNotEmpty_OperandAndRightParenAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.CurrentExpression = new List<string> { "(" };
        infExpProcessor.AddParen();

        Assert.AreEqual("( 5 )",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithOperatorAndCurrentOperandEmpty_LeftParenAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentExpression = new List<string> { "5", "+" };
        infExpProcessor.AddParen();

        Assert.AreEqual("5 + (",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithOperatorAndCurrentOperandNotEmpty_ExplicitMultiplicationAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.CurrentExpression = new List<string> { "(", "1", "+" };
        infExpProcessor.AddParen();

        Assert.AreEqual("( 1 + 5 * (",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithRightParenAndCurrentOperandEmptyAndMatchedParens_ExplicitMultiplicationAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentExpression = new List<string> { "(", "(", "5", 
            "+", "1", ")", ")" };
        infExpProcessor.AddParen();

        Assert.AreEqual("( ( 5 + 1 ) ) * (",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }

    [Test]
    public void AddParen_ExpressionEndsWithRightParenAndCurrentOperandEmptyAndUnmatchedParens_RightParenAdded()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        infExpProcessor.Initialize();
        infExpProcessor.CurrentExpression = new List<string> { "(", "(", "5", 
            "+", "1", ")" };

        // Since the expression is given instead of construction, artifically
        // add the unmatched left paren count.
        infExpProcessor.UnmatchedLeftParenCount = 1;

        infExpProcessor.AddParen();

        Assert.AreEqual("( ( 5 + 1 ) )",
            string.Join(" ", infExpProcessor.CurrentExpression));
    }
    #endregion

    #region AddToCurrentInput Tests
    [Test]
    public void AddToCurrentInput_InputIsDecimalAndCurrentOperandEmpty_AddsZeroPlusDecimal()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.AddToCurrentInput(".");
        Assert.AreEqual("0.", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndCurrentOperandNotEmptyAndCurrentOperandDecimal_DoesNotAddDecimal()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5.";
        infExpProcessor.AddToCurrentInput(".");
        Assert.AreEqual("5.", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndCurrentOperandNotEmptyAndCurrentOperandNoDecimal_AddsDecimal()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.AddToCurrentInput(".");
        Assert.AreEqual("5.", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandNotEmptyAndCurrentOperandNotZero_AddInput()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "0";
        infExpProcessor.AddToCurrentInput("5");
        Assert.AreEqual("5", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandNotEmptyAndCurrentOperandZero_ChangeToInput()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "0";
        infExpProcessor.AddToCurrentInput("5");
        Assert.AreEqual("5", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandNotEmptyAndCurrentOperandIsResult_ChangeToInput()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.IsResult = true;
        infExpProcessor.CurrentOperand = "1";
        infExpProcessor.AddToCurrentInput("5");
        Assert.AreEqual("5", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandEmpty_CurrentOperandIsZero()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.AddToCurrentInput("0");
        Assert.AreEqual("0", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandNotEmptyAndCurrentOperandNotZero_AddsZero()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.AddToCurrentInput("0");
        Assert.AreEqual("50", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandNotEmptyAndCurrentOperandZero_DoesNotAddZero()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "0";
        infExpProcessor.AddToCurrentInput("0");
        Assert.AreEqual("0", infExpProcessor.CurrentOperand);
    }
    #endregion

    #region Backspace Tests
    [Test]
    public void Backspace_CurrentOperandNotEmpty_RemovesLastToken()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "506";
        infExpProcessor.Backspace();
        Assert.AreEqual("50", infExpProcessor.CurrentOperand);
    }
    #endregion

    #region ClearInput Tests
    [Test]
    public void ClearInput_CurrentOperandEmptyAndClearedOnce_ClearsExpression()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentExpression = new List<string>() { "1", "+", "2" };
        infExpProcessor.CurrentOperand = "";
        infExpProcessor.ClearedOnce = true;
        infExpProcessor.ClearInput();
        Assert.AreEqual(0, infExpProcessor.CurrentExpression.Count);
    }

    [Test]
    public void ClearInput_CurrentOperandEmptyAndNotClearedOnce_ClearsOperand()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "";
        infExpProcessor.ClearedOnce = false;
        infExpProcessor.ClearInput();
        Assert.AreEqual("", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void ClearInput_CurrentOperandNotEmpty_ClearsOperand()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "500";
        infExpProcessor.ClearInput();
        Assert.AreEqual("", infExpProcessor.CurrentOperand);
    }
    #endregion

    #region Execute Tests
    [Test]
    public void Execute_ComplexExpressions_ResultIsCorrect()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();

        List<List<string>> infixExpressions = new List<List<string>>()
        {
            // Handles decimal values
            new List<string>(){"6.23", "+", "0.019", "*", "(", "23.41", "^",
                "2.03", ")"},
            // Handles negative decimal values
            new List<string>(){"-6.23", "+", "(", "-0.019", ")", "*", "(",
                "23.41", "^", "-2.03", ")"},
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
            infExpProcessor.Initialize();
            infExpProcessor.CurrentExpression = infixExpressions[i];
            infExpProcessor.Execute();
            Assert.AreEqual(expectedResults[i], infExpProcessor.CurrentOperand);
        }

    }
    #endregion

    #region ToggleNegative Tests
    [Test]
    public void ToggleNegative_CurrentOperandNotEmptyAndCurrentOperandNegative_OperandPositive()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "-5";
        infExpProcessor.ToggleNegative();
        Assert.AreEqual("5", infExpProcessor.CurrentOperand);
    }

    [Test]
    public void ToggleNegative_CurrentOperandNotEmptyAndCurrentOperandNotNegative_NegatesOperand()
    {
        GameObject go = new GameObject();
        InfixExpressionProcessor infExpProcessor = go.AddComponent<InfixExpressionProcessor>();
        infExpProcessor.Initialize();
        infExpProcessor.CurrentOperand = "5";
        infExpProcessor.ToggleNegative();
        Assert.AreEqual("-5", infExpProcessor.CurrentOperand);
    }    
    #endregion

}
