using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Reads all custom operations from their file and populates the custom
/// operation selector with their information.
/// </summary>
public class CustomOperationLoader : MonoBehaviour
{
    /// <summary>
    /// Prefab representing the button used to select a custom operation.
    /// </summary>
    [Tooltip("Prefab representing the button used to select a custom " + 
        "operation.")]
    [SerializeField] private GameObject selectButtonPrefab;

    /// <summary>
    /// Transform whose children will represent custom operation options.
    /// </summary>
    [Tooltip("Transform whose children will represent custom operation " + 
        "options.")]
    [SerializeField] private RectTransform contentTransform;

    #region MonoBehaviour Methods
    private void Start()
    {
        LoadCustomOperations();
    }
    #endregion

    /// <summary>
    /// Reads all custom operation ScriptableObject files from the Resources 
    /// folder.
    /// </summary>
    private void LoadCustomOperations()
    {
        Object[] customOperationObjects =
                    Resources.LoadAll("CustomOperations", typeof(ScriptableObject));

        foreach (Object operation in customOperationObjects)
        {
            ICustomOperation customOp = operation as ICustomOperation;
            GameObject buttonObject = Instantiate(selectButtonPrefab, contentTransform);
            OperationSelectionButton opSelButton =
                buttonObject.GetComponent<OperationSelectionButton>();
            opSelButton.CustomOperation = customOp;
        }
    }
}
