using System.Collections.Generic;
using UnityEngine;

// TODO: Unit test this class. Perhaps I can pull out a few more operations from
// InputProcessor first.

/// <summary>
/// Receives input from the calculator GUI to use in custom operations 
/// </summary>
public class CustomOperationInputProcessor : MonoBehaviour
{
    #region Delegates
    /// <summary>
    /// Delegate to signal an update to the clear button.
    /// </summary>
    public UpdateClear UpdateClear;

    /// <summary>
    /// Delegate to signal a current operand update.
    /// </summary>
    public UpdateCurrentOperand UpdateCurrentOperand;

    /// <summary>
    /// Delegate to signal an error update.
    /// </summary>
    public UpdateError UpdateError;
    #endregion

    /// <summary>
    /// Event used to signal the custom operation has been executed.
    /// </summary>
    [Tooltip("Event used to signal the custom operation has been executed.")]
    [SerializeField] private GameEvent ExecuteCustomOperationEvent;

    /// <summary>
    /// Event used to signal the custom operation has been been canceled
    /// </summary>
    [Tooltip("Event used to signal the custom operation has been canceled.")]
    [SerializeField] private GameEvent HideCustomOperationDialogEvent;

    /// <summary>
    /// Input Processor handling the infix expression evaluation of the
    /// calculator.
    /// </summary>
    [Tooltip("Input Processor handling the infix expression evaluation of " +
        "the calculator.")]
    [SerializeField] private InputProcessor inputProcessor;

    /// <summary>
    /// Array of arguments to be passed to the custom operation.
    /// </summary>
    private string[] argumentArray;

    /// <summary>
    /// The index of the argument of the custom operation that is currently
    /// accepting input.
    /// </summary>
    private int currentArgumentIndex;

    /// <summary>
    /// Operand the user is currently entering.
    /// </summary>
    private string currentOperand;

    /// <summary>
    /// Custom Operation the processor is currently concerned with.
    /// </summary>
    private ICustomOperation customOperation;

    /// <summary>
    /// Number of arguments in the custom operation.
    /// </summary>
    private int numArgs;

    /// <summary>
    /// List of argument display objects that will allow the user to enter 
    /// arguments for the custom operation.
    /// </summary>
    private List<ArgumentDisplay> argumentDisplays;

    /// <summary>
    /// Custom Operation the processor is currently concerned with.
    /// </summary>
    public ICustomOperation CustomOperation
    {
        get
        {
            return customOperation;
        }
        set
        {
            customOperation = value;

            numArgs = customOperation.ArgumentLabels.Length;
            argumentArray = new string[numArgs];
            currentArgumentIndex = 0;
            currentOperand = "";
        }
    }

    /// <summary>
    /// List of argument display objects that will allow the user to enter 
    /// arguments for the custom operation.
    /// </summary>
    public List<ArgumentDisplay> ArgumentDisplays
    {
        get
        {
            return argumentDisplays;
        }
        set
        {
            argumentDisplays = value;
            argumentDisplays[0].Activate();
        }
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
            if (customOperation.AllowsDecimal)
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
            else
            {
                if (currentOperand == "")
                {
                    currentOperand = "0.";
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

        UpdateClear?.Invoke(currentOperand);
        ArgumentDisplays[currentArgumentIndex].SetArgumentValueText(currentOperand);
    }

    /// <summary>
    /// Deletes the most recent character in the current operand.
    /// </summary>
    public void Backspace()
    {
        if (currentOperand == "")
        {
            ArgumentDisplays[currentArgumentIndex].SetArgumentValueText("0");
        }
        else
        {
            currentOperand =
                currentOperand.Substring(0, currentOperand.Length - 1);
            if (currentOperand == "")
            {
                ArgumentDisplays[currentArgumentIndex].SetArgumentValueText("0");
                UpdateClear?.Invoke(currentOperand);
            }
            else
            {
                ArgumentDisplays[currentArgumentIndex].SetArgumentValueText(currentOperand);
            }
        }
    }

    /// <summary>
    /// If an operand exists, clears the operand and sets the custom operation's 
    /// argument display value to read "O." If an operand does not exist,moves 
    /// focus to the previous argument's value field and allows for operand 
    /// entry. If there are no previous arguments, exits the custom operation 
    /// dialog.
    /// </summary>
    public void ClearInput()
    {
        if (currentOperand == "")
        {
            argumentDisplays[currentArgumentIndex].Deactivate();
            currentArgumentIndex--;
            if (currentArgumentIndex < 0)
            {
                HideCustomOperationDialogEvent.Raise();
            }
            else
            {
                argumentDisplays[currentArgumentIndex].Activate();
                currentOperand =
                    argumentDisplays[currentArgumentIndex].GetArgumentValueText();
            }
        }
        else
        {
            currentOperand = "";
            ArgumentDisplays[currentArgumentIndex].SetArgumentValueText("0");
        }

        UpdateClear?.Invoke(currentOperand);
    }

    /// <summary>
    /// If the custom operation has more arguments, moves the focus of the 
    /// processor to the next argument. Otherwise, executes the custom operation
    /// with given arguments.
    /// </summary>
    public void Enter()
    {
        if (currentArgumentIndex < numArgs - 1)
        {
            argumentArray[currentArgumentIndex] = currentOperand;
            argumentDisplays[currentArgumentIndex].Deactivate();

            currentArgumentIndex++;
            currentOperand = "";
            argumentDisplays[currentArgumentIndex].Activate();
            UpdateClear?.Invoke(currentOperand);
        }
        else
        {
            try
            {
                argumentArray[currentArgumentIndex] = currentOperand;
                currentArgumentIndex = 0;
                string result = customOperation.Execute(argumentArray);
                UpdateCurrentOperand?.Invoke(result);
                inputProcessor.CurrentOperand = result;
                UpdateError?.Invoke("");
            }
            catch (CalculatorException e)
            {
                UpdateError?.Invoke(e.Message);
                UpdateCurrentOperand?.Invoke("");
            }
            ExecuteCustomOperationEvent.Raise();
        }
    }

    /// <summary>
    /// Negates the current operand, allowing the user to toggle between the 
    /// negative and positive representation of that operand.
    /// </summary>
    public void ToggleNegative()
    {
        if (customOperation.AllowsNegative)
        {
            if (!(currentOperand == ""))
            {
                if (currentOperand.Substring(0, 1) != "-")
                {
                    currentOperand = "-" + currentOperand;
                }
                else
                {
                    currentOperand =
                        currentOperand.Substring(1, currentOperand.Length - 1);
                }

                ArgumentDisplays[currentArgumentIndex].SetArgumentValueText(currentOperand);
            }
        }
    }
}
