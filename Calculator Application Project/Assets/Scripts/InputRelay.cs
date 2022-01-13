using UnityEngine;

/// <summary>
/// Relays input from the GUI to the appropriate input processor based on the
/// calculator application's current state.
/// </summary>
public class InputRelay : MonoBehaviour
{
    /// <summary>
    /// Is the calculator currently executing a custom operation?
    /// </summary>
    public bool IsCustomOperation { get; set; }

    /// <summary>
    /// Is the calculator currently allowing the user to select a custom 
    /// operation?
    /// </summary>
    public bool IsCustomOperationSelection { get; set; }

    /// <summary>
    /// Input processor for regular infix expression evaluation operations.
    /// </summary>
    [SerializeField] private InputProcessor inputProcessor;

    /// <summary>
    /// Custom operation input processor for all custom operations.
    /// </summary>
    [SerializeField] private CustomOperationInputProcessor customOpProcessor;

    #region MonoBehaviour Methods
    private void Awake()
    {
        IsCustomOperation = false;
        IsCustomOperationSelection = false;
    }
    #endregion

    /// <summary>
    /// Adds an operator to the inputProcessor if the user is not currently 
    /// executing a custom operation. Custom operation do not accept operators,
    /// therefore the method will do nothing if the user is currently executing
    /// a custom operation.
    /// </summary>
    /// <param name="operatorString">Operator to be passed to 
    /// inputProcessor.</param>
    public void AddOperator(string operatorString)
    {
        if (IsCustomOperationSelection || IsCustomOperation)
        {
            return;
        }

        inputProcessor.AddOperator(operatorString);
    }

    /// <summary>
    /// Adds a parenthesis to the inputProcessor if the user is not currently 
    /// executing a custom operation. Custom operation do not accept parens,
    /// therefore the method will do nothing if the user is currently executing
    /// a custom operation.
    /// </summary>
    public void AddParen()
    {
        if (IsCustomOperationSelection || IsCustomOperation)
        {
            return;
        }

        inputProcessor.AddParen();
    }

    /// <summary>
    /// Directs the current input from the calculator GUI to the appropriate 
    /// input processor's "AddToCurrentInput" method based on the calculators
    /// current state.
    /// </summary>
    /// <param name="input">String to be added to the user's current 
    /// input.</param>
    public void AddToCurrentInput(string input)
    {
        if (IsCustomOperationSelection)
        {
            return;
        }

        if (IsCustomOperation)
        {
            customOpProcessor.AddToCurrentInput(input);
        }
        else
        {
            inputProcessor.AddToCurrentInput(input);
        }
    }

    /// <summary>
    /// Calls the backspace method of the appropriate input processor based on 
    /// the calculators current state.
    /// </summary>
    public void Backspace()
    {
        if (IsCustomOperationSelection)
        {
            return;
        }

        if (IsCustomOperation)
        {
            customOpProcessor.Backspace();
        }
        else
        {
            inputProcessor.Backspace();
        }
    }

    /// <summary>
    /// Calls the ClearInput method of the appropriate input processor based on
    /// the calculator's current state.
    /// </summary>
    public void ClearInput()
    {
        if (IsCustomOperationSelection)
        {
            return;
        }

        if (IsCustomOperation)
        {
            customOpProcessor.ClearInput();
        }
        else
        {
            inputProcessor.ClearInput();
        }
    }

    /// <summary>
    /// Calls the Enter/Execute method of the appropriate input processor based 
    /// on the calculator's current state.
    /// </summary>
    public void Enter()
    {
        if (IsCustomOperationSelection)
        {
            return;
        }

        if (IsCustomOperation)
        {
            customOpProcessor.Enter();
        }
        else
        {
            inputProcessor.Execute();
        }
    }

    /// <summary>
    /// Calls the ToggleNegative method of the appropriate input processor based 
    /// on the calculator's current state.
    /// </summary>
    public void ToggleNegative()
    {
        if (IsCustomOperationSelection)
        {
            return;
        }

        if (IsCustomOperation)
        {
            customOpProcessor.ToggleNegative();
        }
        else
        {
            inputProcessor.ToggleNegative();
        }
    }
}