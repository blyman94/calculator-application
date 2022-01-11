using System.Collections;
using UnityEngine;

/// <summary>
/// Utility to show and hide CanvasGroups representing menus in the game's UI.
/// Works very cleanly with GameEventListeners.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupRevealer : MonoBehaviour
{
    /// <summary>
    /// Tracks whether the Canvas group is currently shown or hidden.
    /// </summary>
    public bool Shown { get; set; } = true;

    /// <summary>
    /// CanvasGroup to be controlled by the revealer.
    /// </summary>
    [Tooltip("CanvasGroup to be controlled by the revealer.")]
    [SerializeField] private CanvasGroup canvasGroup;

    /// <summary>
    /// Should this canvas group fade in and out?
    /// </summary>
    [Tooltip("Should this canvas group fade in and out?")]
    [SerializeField] private bool canvasFade;

    /// <summary>
    /// How long should it take for this canvas to fade in?
    /// </summary>
    [Tooltip("How long should it take for this canvas to fade in?")]
    [SerializeField] private float FadeInTime;

    /// <summary>
    /// How long should it take for this canvas to fade out?
    /// </summary>
    [Tooltip("How long should it take for this canvas to fade out?")]
    [SerializeField] private float FadeOutTime;

    /// <summary>
    /// Should this CanvasGroup start hidden?
    /// </summary>
    [Tooltip("Should this CanvasGroup start hidden?")]
    [SerializeField] private bool startHidden = false;

    /// <summary>
    /// The alpha value this CanvasGroup will have when in the shown state.
    /// </summary>
    [Tooltip("The alpha value this CanvasGroup will have when in the shown " +
        "state")]
    [SerializeField, Range(0.0f, 1.0f)] private float shownAlpha = 1;

    /// <summary>
    /// The BlockRaycast value this CanvasGroup will have when in the 
    /// shown state.
    /// </summary>
    [Tooltip("The BlockRaycast value this CanvasGroup will have when in the" +
        "shown state.")]
    [SerializeField] private bool shownBlockRaycast = true;

    /// <summary>
    /// The Interactable value this Canvas group will have when in 
    /// the shown state.
    /// </summary>
    [Tooltip("The Interactable value this Canvas group will have when in the " +
        "shown state.")]
    [SerializeField] private bool shownInteractable = true;

    #region MonoBehaviour Methods
    private void Awake()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    private void Start()
    {
        if (startHidden)
        {
            Shown = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
    #endregion

    /// <summary>
    /// Shows the CanvasGroup if its hidden, hides the canvas group if its 
    /// shown.
    /// </summary>
    public void Toggle()
    {
        if (Shown)
        {
            HideGroup();
        }
        else
        {
            ShowGroup();
        }
    }

    /// <summary>
    /// Hides the CanvasGroup. Alpha is set to zero, BlockRaycasts is set to 
    /// false and Interactable is set to false.
    /// </summary>
    public void HideGroup()
    {
        if (canvasFade)
        {
            StartCoroutine(HideGroupRoutine(FadeOutTime));
        }
        else
        {
            Shown = false;
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }

    /// <summary>
    /// Hides the group by fading it out over time. Assigns shown state 
    /// parameters Alpha, BlockRaycast and Interactable.
    /// </summary>
    /// <param name="fadeTime">The amount of time to fade out the group.</param>
    private IEnumerator HideGroupRoutine(float fadeTime)
    {
        float elapsedTime = 0.0f;
        float currentAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha =
                Mathf.Lerp(currentAlpha, 0.0f, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Shown = false;
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
    }

    /// <summary>
    /// Shows the CanvasGroup. Assigns shown state parameters Alpha,
    /// BlockRaycast and Interactable.
    /// </summary>
    public void ShowGroup()
    {
        if (canvasFade)
        {
            StartCoroutine(ShowGroupRoutine(FadeInTime));
        }
        else
        {
            Shown = true;
            canvasGroup.alpha = shownAlpha;
            canvasGroup.blocksRaycasts = shownBlockRaycast;
            canvasGroup.interactable = shownInteractable;
        }
    }

    /// <summary>
    /// Shows the CanvasGroup by fading it in over time. Assigns shown state 
    /// parameters Alpha, BlockRaycast and Interactable.
    /// </summary>
    private IEnumerator ShowGroupRoutine(float fadeTime)
    {
        Shown = true;
        canvasGroup.blocksRaycasts = shownBlockRaycast;
        canvasGroup.interactable = shownInteractable;

        float elapsedTime = 0.0f;
        float currentAlpha = canvasGroup.alpha;

        while (elapsedTime < fadeTime)
        {
            canvasGroup.alpha =
                Mathf.Lerp(currentAlpha, shownAlpha, elapsedTime / fadeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = shownAlpha;
    }
}