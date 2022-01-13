using TMPro;
using UnityEngine;

/// <summary>
/// Responsible for updating the calculator GUI in response to changes in the 
/// backend input processors.
/// </summary>
public class CalculatorGuiUpdater : MonoBehaviour
{
    /// <summary>
    /// Text object that displays the operand the user is currently entering.
    /// </summary>
    [Tooltip("Text object that displays the operand the user is " +
        "currently entering.")]
    [SerializeField] private TextMeshProUGUI currentOperandText;

    /// <summary>
    /// Text object that displays the state of the clear button based on current
    /// input.
    /// </summary>
    [Tooltip("Text object that displays the state of the clear button " +
        "based on current input.")]
    [SerializeField] private TextMeshProUGUI clearClearEntryText;

    /// <summary>
    /// Text object that displays the expression the user is currently entering.
    /// </summary>
    [Tooltip("Text object that displays the expression the user is " +
        "currently entering.")]
    [SerializeField] private TextMeshProUGUI currentExpressionText;

    /// <summary>
    /// Text object that displays the current error to the user.
    /// </summary>
    [Tooltip("Text object that displays the current error to the user.")]
    [SerializeField] private TextMeshProUGUI errorDisplayText;

    /// <summary>
    /// Input processor based on which this class will update the GUI.
    /// </summary>
    [Tooltip("Input processor based on which this class will update the GUI.")]
    [SerializeField] private InputProcessor inputProcessor;

    /// <summary>
    /// Input processor based on which this class will update the GUI.
    /// </summary>
    [Tooltip("Input processor based on which this class will update the GUI.")]
    [SerializeField]
    private CustomOperationInputProcessor customOperationInputProcessor;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        SubscribeToDelegates();
    }

    private void OnDisable()
    {
        UnsubscribeFromDelegates();
    }
    #endregion

    /// <summary>
    /// TODO: Subscribes to all delegates of interest from the InputProcessor and 
    /// CustomOperationInputProcessor classes.
    /// </summary>
    private void SubscribeToDelegates()
    {
        if (inputProcessor != null)
        {
            inputProcessor.UpdateClear += UpdateClearText;
            inputProcessor.UpdateCurrentExpression += UpdateExpressionText;
            inputProcessor.UpdateCurrentOperand += UpdateCurrentOperandText;
            inputProcessor.UpdateError += UpdateErrorText;
        }
        if (customOperationInputProcessor != null)
        {
            customOperationInputProcessor.UpdateClear += UpdateClearText;
            customOperationInputProcessor.UpdateCurrentOperand +=
                UpdateCurrentOperandText;
            customOperationInputProcessor.UpdateError += UpdateErrorText;
        }
    }

    /// <summary>
    /// TODO: Unsubscribes from all delegates of interest from the InputProcessor 
    /// and CustomOperationInputProcessor classes.
    /// </summary>
    private void UnsubscribeFromDelegates()
    {
        if (inputProcessor != null)
        {
            inputProcessor.UpdateClear -= UpdateClearText;
            inputProcessor.UpdateCurrentExpression -= UpdateExpressionText;
            inputProcessor.UpdateCurrentOperand -= UpdateCurrentOperandText;
            inputProcessor.UpdateError -= UpdateErrorText;
        }
        if (customOperationInputProcessor != null)
        {
            customOperationInputProcessor.UpdateClear -= UpdateClearText;
            customOperationInputProcessor.UpdateCurrentOperand -=
                UpdateCurrentOperandText;
            customOperationInputProcessor.UpdateError -= UpdateErrorText;
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
    private void UpdateClearText(string currentOperand)
    {
        if (clearClearEntryText != null)
        {
            if (currentOperand == "")
            {
                clearClearEntryText.text = "C";
            }
            else
            {
                clearClearEntryText.text = "CE";
            }
        }
    }

    /// <summary>
    /// Updates the GUI to reflect the current operand string.
    /// </summary>
    /// <param name="currentOperand">String to display as current operand 
    /// in the GUI.</param>
    private void UpdateCurrentOperandText(string currentOperand)
    {
        if (currentOperandText != null)
        {
            if (currentOperand == "" && currentOperandText.text != "0")
            {
                currentOperandText.text = "0";
            }
            else
            {
                currentOperandText.text = currentOperand;
            }
        }
    }

    /// <summary>
    /// Updates the calculator's error display to reflect the passed message.
    /// </summary>
    /// <param name="errorMessage">String containing a message to be displayed
    /// in the error text object.</param>
    private void UpdateErrorText(string errorMessage)
    {
        if (errorDisplayText != null)
        {
            errorDisplayText.text = errorMessage;
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
