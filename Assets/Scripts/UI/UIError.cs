using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Shown when exceptions remain unhandled.
/// </summary>
public class UIError : MonoBehaviour
{
    [SerializeField]
    private Text UIException;
    [SerializeField]
    private NavigationController NavigationController;

    private Animator animator;
    private Canvas canvas;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        canvas = GetComponent<Canvas>();
    }

    /// <summary>
    /// Shows the UI and sets the error text.
    /// </summary>
    /// <param name="error">Exception message to show.</param>
    public void Show(string error)
    {
        UIException.text = error;
        canvas.sortingOrder = 1;
        NavigationController.ShowError(GetComponent<Animator>());
        NavigationController.SetRenderMode(RenderMode.ScreenSpaceOverlay);
    }

    /// <summary>
    /// Hides the UI.
    /// </summary>
    public void Exit()
    {
        NavigationController.ExitError(GetComponent<Animator>());
    }

    /// <summary>
    /// Sets the sorting order to enable interaction with other screens.
    /// </summary>
    public void SendToBack()
    {
        canvas.sortingOrder = -1;
        NavigationController.SetRenderMode(RenderMode.ScreenSpaceCamera);
    }
}
