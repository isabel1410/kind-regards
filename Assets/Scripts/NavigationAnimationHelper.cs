using UnityEngine;

/// <summary>
/// Handles animation events from "Navigation Animator Controller".
/// </summary>
public class NavigationAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private UIRequest uiRequest;
    [SerializeField]
    private UIReply uiReply;
    [SerializeField]
    private UIMailbox uiMailbox;

    /// <summary>
    /// Sets the rendermode of the UI.
    /// </summary>
    /// <param name="renderMode">The render mode to use.</param>
    public void SetRenderMode(RenderMode renderMode)
    {
        navigationController.SetRenderMode(renderMode);
    }

    /// <summary>
    /// Transitions from the home screen to the requests screen.
    /// </summary>
    public void HomeToRequests()
    {
        navigationController.HomeToRequests();
    }

    /// <summary>
    /// Transitions from the home screen to the reply screen.
    /// </summary>
    public void HomeToReply()
    {
        navigationController.HomeToReply();
    }

    /// <summary>
    /// Transitions from the home screen to the customization screen.
    /// </summary>
    public void HomeToCustomization()
    {
        navigationController.HomeToCustomization();
    }

    /// <summary>
    /// Destroys the request buttons from the requests screen.
    /// </summary>
    public void DestroyRequestButtons()
    {
        uiRequest.DestroyRequestButtons();
    }

    /// <summary>
    /// Destroys the reqply buttons from the reply screen.
    /// </summary>
    public void DestroyReplyButtons()
    {
        uiReply.DestroyReplyButtons();
    }

    /// <summary>
    /// Destroys the mail gameobjects from the mailbox screen.
    /// </summary>
    public void DestroyMailboxGameObjects()
    {
        uiMailbox.DestroyMailboxGameObjects();
    }
}
