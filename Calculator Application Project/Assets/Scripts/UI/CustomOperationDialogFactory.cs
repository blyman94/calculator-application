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
    /// Custom input processor that will drive GUI updates during the custom
    /// operation execution.
    /// </summary>
    [SerializeField]
    [Tooltip("Custom input processor that will drive GUI updates during " +
        "the custom operation execution.")]
    private CustomOperationInputProcessor customOperationInputProcessor;

    /// <summary>
    /// Prefab representing an argument display to be shown in the custom 
    /// operation dialog.
    /// </summary>
    [Tooltip("Prefab representing an argument display to be shown in the " +
        " custom operation dialog.")]
    [SerializeField] private GameObject argumentDisplayPrefab;

    /// <summary>
    /// Transform whose children will represent custom operation arguments.
    /// </summary>
    [Tooltip("Transform whose children will represent custom operation " +
        "arguments.")]
    [SerializeField] private RectTransform argumentContentTransform;

    /// <summary>
    /// Text object representing the title of the custom operation dialog.
    /// </summary>
    [Tooltip("Text object representing the title of the custom operation " +
        "dialog.")]
    [SerializeField] TextMeshProUGUI titleText;

    /// <summary>
    /// Text object representing the description of the custom operation dialog.
    /// </summary>
    [Tooltip("Text object representing the description of the custom " +
        "operation dialog.")]
    [SerializeField] TextMeshProUGUI descriptionText;

    // <summary>
    /// Text object representing the instructions of the custom operation 
    /// dialog.
    /// </summary>
    [Tooltip("Text object representing the instructions of the custom " +
        "operation dialog.")]
    [SerializeField] TextMeshProUGUI instructionText;

    /// <summary>
    /// List of argument display objects that will allow the user to enter 
    /// arguments for the custom operation.
    /// </summary>
    private List<ArgumentDisplay> argumentDisplays = 
        new List<ArgumentDisplay>();

    /// <summary>
    /// Changes the custom operation dialogue display to reflect the user 
    /// selected custom operation.
    /// </summary>
    /// <param name="customOperation">ICustomOperation that has been 
    /// selected by the user.</param>
    public void MakeDialog(ICustomOperation customOperation)
    {
        argumentDisplays.Clear();

        // TODO: Object pooling for argument display prefabs.
        int childCount = argumentContentTransform.childCount;
        if (childCount > 0)
        {
            for (int i = childCount - 1; i >= 0; i--)
            {
                Destroy(argumentContentTransform.GetChild(i).gameObject);
            }
        }

        titleText.text = customOperation.Name;
        descriptionText.text = customOperation.Description;
        instructionText.text = customOperation.Instructions;

        foreach (string argumentLabel in customOperation.ArgumentLabels)
        {
            GameObject argumentDisplayObject =
                Instantiate(argumentDisplayPrefab, argumentContentTransform);
            ArgumentDisplay argDisplay =
                argumentDisplayObject.GetComponent<ArgumentDisplay>();
            argDisplay.SetArgumentLabelText(argumentLabel);
            argumentDisplays.Add(argDisplay);
        }

        customOperationInputProcessor.CustomOperation = customOperation;
        customOperationInputProcessor.ArgumentDisplays = argumentDisplays;
    }
}