using TMPro;
using UnityEngine;

public class CustomOperationDialogFactory : MonoBehaviour
{
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
    /// Changes the custom operation dialogue display to reflect the user 
    /// selected custom operation.
    /// </summary>
    /// <param name="customOperation">ICustomOperation that has been 
    /// selected by the user.</param>
    public void MakeDialog(ICustomOperation customOperation)
    {
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
        }
    }
}
