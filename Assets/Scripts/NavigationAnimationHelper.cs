using UnityEngine;

/// <summary>
/// Handles animation events from "Navigation Animator Controller".
/// </summary>
public class NavigationAnimationHelper : MonoBehaviour
{
    public NavigationController NavigationController;
    public UIRequest UIRequest;
    public UIMailbox UIMailbox;

    /// <summary>
    /// Sets the rendermode of the UI.
    /// </summary>
    /// <param name="renderMode">The render mode to use.</param>
    public void SetRenderMode(RenderMode renderMode)
    {
        NavigationController.SetRenderMode(renderMode);
    }

    /// <summary>
    /// Transition from the home screen to the requests screen.
    /// </summary>
    public void HomeToRequests()
    {
        NavigationController.HomeToRequests();
    }

    /// <summary>
    /// Destroy the request buttons from the requests screen.
    /// </summary>
    public void DestroyRequestButtons()
    {
        UIRequest.DestroyRequestButtons();
    }

    /// <summary>
    /// Destroys the mail gameobjects from the mailbox screen.
    /// </summary>
    public void DestroyMailboxGameObjects()
    {
        UIMailbox.DestroyMailboxGameObjects();
    }
}
