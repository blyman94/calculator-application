using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Receives input from the calculator GUI to use in custom operations 
/// </summary>
public class CustomOperationProcessor : MonoBehaviour, IInputProcessor
{
    /// <summary>
    /// The index of the argument of the custom operation that is currently
    /// accepting input.
    /// </summary>
    public int CurrentArgumentIndex { get; set; }

    /// <summary>
    /// Number of arguments in the custom operation.
    /// </summary>
    public int NumArgs { get; set; }

    /// <summary>
    /// Input Processor handling the infix expression evaluation of the
    /// calculator.
    /// </summary>
    [Tooltip("Input Processor handling the infix expression evaluation of " +
        "the calculator.")]
    [SerializeField] private InfixExpressionProcessor infixExpressionProcessor;

    [Header("Events")]
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
    /// Array of arguments to be passed to the custom operation.
    /// </summary>
    private string[] argumentArray;

    /// <summary>
    /// List of argument display objects that will allow the user to enter 
    /// arguments for the custom operation.
    /// </summary>
    private List<ArgumentDisplay> argumentDisplays;

    /// <summary>
    /// Custom Operation the processor is currently concerned with.
    /// </summary>
    private ICustomOperation customOperation;

    #region IInputProcessor Methods
    public string CurrentOperand { get; set; }

    public UpdateClear UpdateClear { get; set; }

    public UpdateCurrentOperand UpdateCurrentOperand { get; set; }

    public UpdateCurrentExpression UpdateCurrentExpression { get; set; }

    public UpdateError UpdateError { get; set; }
    #endregion

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

            NumArgs = customOperation.ArgumentLabels.Length;
            argumentArray = new string[NumArgs];
            CurrentArgumentIndex = 0;
            CurrentOperand = "";
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
            else
            {
                if (CurrentOperand == "")
                {
                    CurrentOperand = "0";
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
            if (CurrentOperand == "0")
            {
                CurrentOperand = input;
            }
            else
            {
                CurrentOperand += input;
            }
        }

        UpdateClear?.Invoke(CurrentOperand);

        if (ArgumentDisplays != null)
        {
            ArgumentDisplays[CurrentArgumentIndex].SetArgumentValueText(CurrentOperand);
        }
    }

    /// <summary>
    /// Deletes the most recent character in the current operand.
    /// </summary>
    public void Backspace()
    {
        if (CurrentOperand == "")
        {
            if (ArgumentDisplays != null)
            {
                ArgumentDisplays[CurrentArgumentIndex].SetArgumentValueText("0");
            }
        }
        else
        {
            CurrentOperand =
                CurrentOperand.Substring(0, CurrentOperand.Length - 1);

            if (CurrentOperand == "")
            {
                if (ArgumentDisplays != null)
                {
                    ArgumentDisplays[CurrentArgumentIndex].SetArgumentValueText("0");
                }
                UpdateClear?.Invoke(CurrentOperand);
            }
            else
            {
                if (ArgumentDisplays != null)
                {
                    ArgumentDisplays[CurrentArgumentIndex].SetArgumentValueText(CurrentOperand);
                }
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
        if (CurrentOperand == "")
        {
            if (argumentDisplays != null)
            {
                argumentDisplays[CurrentArgumentIndex].Deactivate();
            }
            CurrentArgumentIndex--;
            if (CurrentArgumentIndex < 0)
            {
                HideCustomOperationDialogEvent.Raise();
            }
            else
            {
                if (argumentDisplays != null)
                {
                    argumentDisplays[CurrentArgumentIndex].Activate();
                    CurrentOperand =
                        argumentDisplays[CurrentArgumentIndex].GetArgumentValueText();
                }
            }
        }
        else
        {
            CurrentOperand = "";

            if (ArgumentDisplays != null)
            {
                ArgumentDisplays[CurrentArgumentIndex].SetArgumentValueText("0");
            }
        }

        UpdateClear?.Invoke(CurrentOperand);
    }

    /// <summary>
    /// If the custom operation has more arguments, moves the focus of the 
    /// processor to the next argument. Otherwise, executes the custom operation
    /// with given arguments.
    /// </summary>
    public void Enter()
    {
        if (CurrentArgumentIndex < NumArgs - 1)
        {
            if (argumentArray != null)
            {
                argumentArray[CurrentArgumentIndex] = CurrentOperand;
            }

            if (argumentDisplays != null)
            {
                argumentDisplays[CurrentArgumentIndex].Deactivate();
            }

            CurrentArgumentIndex++;
            CurrentOperand = "";

            if (argumentDisplays != null)
            {
                argumentDisplays[CurrentArgumentIndex].Activate();
            }
            
            UpdateClear?.Invoke(CurrentOperand);
        }
        else
        {
            try
            {
                argumentArray[CurrentArgumentIndex] = CurrentOperand;
                CurrentArgumentIndex = 0;
                string result = customOperation.Execute(argumentArray);
                UpdateCurrentOperand?.Invoke(result);
                infixExpressionProcessor.CurrentOperand = result;
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

                if (ArgumentDisplays != null)
                {
                    ArgumentDisplays[CurrentArgumentIndex].SetArgumentValueText(CurrentOperand);
                }
            }
        }
    }
}
