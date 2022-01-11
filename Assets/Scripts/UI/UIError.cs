using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shown when exceptions remain unhandled.
/// </summary>
public class UIError : MonoBehaviour
{
    [SerializeField]
    private Text uiException;
    [SerializeField]
    private Button uiButton;
    [SerializeField]
    private NavigationController navigationController;

    private Canvas canvas;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    /// <summary>
    /// Shows the UI and sets the error text.
    /// </summary>
    /// <param name="error">Exception message to show.</param>
    public void Show(string error)
    {
        uiButton.interactable = true;
        uiException.text = error;
        canvas.sortingOrder = 1;
        navigationController.ShowError(GetComponent<Animator>());
        navigationController.SetRenderMode(RenderMode.ScreenSpaceOverlay);
    }

    /// <summary>
    /// Hides the UI.
    /// </summary>
    public void Exit()
    {
        uiButton.interactable = false;
        navigationController.ExitError(GetComponent<Animator>());
    }

    /// <summary>
    /// Sets the sorting order to enable interaction with other screens.
    /// </summary>
    public void SendToBack()
    {
        canvas.sortingOrder = -1;
        navigationController.SetRenderMode(RenderMode.ScreenSpaceCamera);
    }
}
