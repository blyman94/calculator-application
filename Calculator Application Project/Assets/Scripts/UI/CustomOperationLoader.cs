using UnityEngine;

/// <summary>
/// Reads all custom operations from their file and populates the custom
/// operation selector with their information.
/// </summary>
public class CustomOperationLoader : MonoBehaviour
{
    /// <summary>
    /// Object pooler for OperationSelectionButtons.
    /// </summary>
    [Tooltip("Object pooler for OperationSelectionButtons.")]
    [SerializeField] private ObjectPooler selectionButtonPooler;

    [Header("GUI Elements")]
    /// <summary>
    /// Transform whose children will represent custom operation options.
    /// </summary>
    [Tooltip("Transform whose children will represent custom operation " +
        "options.")]
    [SerializeField] private RectTransform selectionButtonContentTransform;

    #region MonoBehaviour Methods
    private void Start()
    {
        if (selectionButtonContentTransform != null)
        {
            selectionButtonPooler.InitializePool(selectionButtonContentTransform);
        }
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
                    Resources.LoadAll("CustomOperations",
                    typeof(ScriptableObject));

        if (selectionButtonPooler != null)
        {
            selectionButtonPooler.DeactivateAll();

            foreach (Object operation in customOperationObjects)
            {
                ICustomOperation customOp = operation as ICustomOperation;
                GameObject buttonObject = selectionButtonPooler.GetObject();
                OperationSelectionButton opSelButton =
                    buttonObject.GetComponent<OperationSelectionButton>();
                opSelButton.CustomOperation = customOp;

                buttonObject.SetActive(true);
            }
        }
    }
}
