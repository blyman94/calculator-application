using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using NSubstitute;

public class CustomOperationInputProcessorTest
{
    #region AddToCurrentInput Tests
    [Test]
    public void AddToCurrentInput_InputIsDecimalAndAllowsDecimalAndCurrentOperandEmpty_AddZeroAndDecimal()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor = 
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsDecimal.Returns(true);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "";

        customOpProcessor.AddToCurrentInput(".");

        Assert.AreEqual("0.", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndAllowsDecimalAndCurrentOperandNotEmptyAndContainsDecimal_NoChange()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsDecimal.Returns(true);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "5.";

        customOpProcessor.AddToCurrentInput(".");

        Assert.AreEqual("5.", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndAllowsDecimalAndCurrentOperandNotEmptyAndDoesNotContainDecimal_AddDecimal()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsDecimal.Returns(true);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "5";

        customOpProcessor.AddToCurrentInput(".");

        Assert.AreEqual("5.", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndDoesNotAllowDecimalAndCurrentOperandEmpty_IsZero()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsDecimal.Returns(false);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "";

        customOpProcessor.AddToCurrentInput(".");

        Assert.AreEqual("0", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsDecimalAndDoesNotAllowDecimalAndCurrentOperandNotEmpty_NoChange()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsDecimal.Returns(false);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "5";

        customOpProcessor.AddToCurrentInput(".");

        Assert.AreEqual("5", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandIsZero_ChangeToInput()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "0";

        customOpProcessor.AddToCurrentInput("5");

        Assert.AreEqual("5", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsIntegerAndCurrentOperandIsNotZero_AddInput()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "1";

        customOpProcessor.AddToCurrentInput("5");

        Assert.AreEqual("15", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandEmpty_IsZero()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "";

        customOpProcessor.AddToCurrentInput("0");

        Assert.AreEqual("0", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandNotEmptyAndCurrentOpreandIsZero_NoChange()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "0";

        customOpProcessor.AddToCurrentInput("0");

        Assert.AreEqual("0", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void AddToCurrentInput_InputIsZeroAndCurrentOperandNotEmptyAndCurrentOpreandNotZero_AddInput()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "1";

        customOpProcessor.AddToCurrentInput("0");

        Assert.AreEqual("10", customOpProcessor.CurrentOperand);
    }
    #endregion

    #region Backspace Tests
    [Test]
    public void Backspace_CurrentOperandNotEmpty_RemovesLastCharacterFromCurrentOperand()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "506";

        customOpProcessor.Backspace();

        Assert.AreEqual("50", customOpProcessor.CurrentOperand);
    }
    #endregion

    #region ClearInput Tests
    [Test]
    public void ClearInput_CurrentOperandEmpty_CurrentArgumentIndexDecrements()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "";
        customOpProcessor.CurrentArgumentIndex = 1;

        customOpProcessor.ClearInput();

        Assert.AreEqual(0, customOpProcessor.CurrentArgumentIndex);
    }

    [Test]
    public void ClearInput_CurrentOperandNotEmpty_CurrentOperandEmpty()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "5";

        customOpProcessor.ClearInput();

        Assert.AreEqual("", customOpProcessor.CurrentOperand);
    }
    #endregion

    #region Enter Tests
    [Test]
    public void Enter_CurrentArgumentIndexLessThanNumArgsMinus1_CurrentArgumentIndexIncrements()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "5";
        customOpProcessor.NumArgs = 3;
        customOpProcessor.CurrentArgumentIndex = 1;

        customOpProcessor.Enter();

        Assert.AreEqual(2, customOpProcessor.CurrentArgumentIndex);
    }

    [Test]
    public void Enter_CurrentArgumentIndexLessThanNumArgsMinus1_CurrentOperandEmpty()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        customOpProcessor.CurrentOperand = "5";
        customOpProcessor.NumArgs = 3;
        customOpProcessor.CurrentArgumentIndex = 1;

        customOpProcessor.Enter();

        Assert.AreEqual("", customOpProcessor.CurrentOperand);
    }
    #endregion

    #region ToggleNegative Tests
    [Test]
    public void ToggleNegative_AllowsNegativeAndOperandNotEmptyAndNotAlreadyNegative_AddNegativeSign()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsNegative.Returns(true);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "5";

        customOpProcessor.ToggleNegative();

        Assert.AreEqual("-5", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void ToggleNegative_AllowsNegativeAndOperandNotEmptyAndAlreadyNegative_RemoveNegativeSign()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsNegative.Returns(true);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "-5";

        customOpProcessor.ToggleNegative();

        Assert.AreEqual("5", customOpProcessor.CurrentOperand);
    }

    [Test]
    public void ToggleNegative_DoesNotAllowNegative_NoChange()
    {
        GameObject go = new GameObject();
        CustomOperationInputProcessor customOpProcessor =
            go.AddComponent<CustomOperationInputProcessor>();

        ICustomOperation customOpSub = Substitute.For<ICustomOperation>();
        customOpSub.AllowsNegative.Returns(false);
        customOpProcessor.CustomOperation = customOpSub;

        customOpProcessor.CurrentOperand = "5";

        customOpProcessor.ToggleNegative();

        Assert.AreEqual("5", customOpProcessor.CurrentOperand);
    }
    #endregion
}
