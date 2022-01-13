using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Makes a custom operation dialog UI based on parameters in a given
/// ICustomOperation class. The creation is triggered through the 
/// CustomOperationEvent architecture.
/// </summary>
public class CustomOperationDialogFactory : MonoBehaviour
{
    /// <summary>
    /// Object pooler for ArgumentDisplayPrefabs.
    /// </summary>
    [Tooltip("Object pooler for ArgumentDisplayPrefabs.")]
    [SerializeField] private ObjectPooler argumentDisplayPooler;

    /// <summary>
    /// Custom input processor that will drive GUI updates during the custom
    /// operation execution.
    /// </summary>
    [SerializeField]
    [Tooltip("Custom input processor that will drive GUI updates during " +
        "the custom operation execution.")]
    private CustomOperationInputProcessor customOperationInputProcessor;

    [Header("GUI Elements")]
    /// <summary>
    /// Transform whose children will represent custom operation arguments.
    /// </summary>
    [Tooltip("Transform whose children will represent custom operation " +
        "arguments.")]
    [SerializeField] private RectTransform argumentContentTransform;

    /// <summary>
    /// Text object representing the description of the custom operation dialog.
    /// </summary>
    [Tooltip("Text object representing the description of the custom " +
        "operation dialog.")]
    [SerializeField] private TextMeshProUGUI descriptionText;

    // <summary>
    /// Text object representing the instructions of the custom operation 
    /// dialog.
    /// </summary>
    [Tooltip("Text object representing the instructions of the custom " +
        "operation dialog.")]
    [SerializeField] private TextMeshProUGUI instructionText;

    /// <summary>
    /// Text object representing the title of the custom operation dialog.
    /// </summary>
    [Tooltip("Text object representing the title of the custom operation " +
        "dialog.")]
    [SerializeField] private TextMeshProUGUI titleText;

    /// <summary>
    /// List of argument display objects that will allow the user to enter 
    /// arguments for the custom operation.
    /// </summary>
    private List<ArgumentDisplay> argumentDisplays;

    #region MonoBehaviour Methods
    private void Start()
    {
        argumentDisplays = new List<ArgumentDisplay>();
        if (argumentDisplayPooler != null && argumentContentTransform != null)
        {
            argumentDisplayPooler.InitializePool(argumentContentTransform);
        }
    }
    #endregion

    /// <summary>
    /// Changes the custom operation dialogue display to reflect the user 
    /// selected custom operation.
    /// </summary>
    /// <param name="customOperation">ICustomOperation that has been 
    /// selected by the user.</param>
    public void MakeDialog(ICustomOperation customOperation)
    {
        argumentDisplays.Clear();

        if (titleText != null)
        {
            titleText.text = customOperation.Name;
        }

        if (descriptionText != null)
        {
            descriptionText.text = customOperation.Description;
        }

        if (instructionText != null)
        {
            instructionText.text = customOperation.Instructions;
        }

        if (argumentDisplayPooler != null)
        {
            argumentDisplayPooler.DeactivateAll();

            foreach (string argumentLabel in customOperation.ArgumentLabels)
            {
                GameObject argumentDisplayObject =
                    argumentDisplayPooler.GetObject();
                ArgumentDisplay argDisplay =
                    argumentDisplayObject.GetComponent<ArgumentDisplay>();

                argDisplay.SetArgumentLabelText(argumentLabel);
                argDisplay.SetArgumentValueText("0");
                argDisplay.Deactivate();
                argumentDisplays.Add(argDisplay);

                argumentDisplayObject.SetActive(true);
            }
        }

        if (customOperationInputProcessor != null)
        {
            customOperationInputProcessor.CustomOperation = customOperation;
            customOperationInputProcessor.ArgumentDisplays = argumentDisplays;
        }
    }
}