using UnityEngine;

/// <summary>
/// Handles animation events from "Navigation Animator Controller".
/// </summary>
public class NavigationAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private NavigationController NavigationController;
    [SerializeField]
    private UIRequest UIRequest;
    [SerializeField]
    private UIReply UIReply;
    [SerializeField]
    private UIMailbox UIMailbox;

    /// <summary>
    /// Sets the rendermode of the UI.
    /// </summary>
    /// <param name="renderMode">The render mode to use.</param>
    public void SetRenderMode(RenderMode renderMode)
    {
        NavigationController.SetRenderMode(renderMode);
    }

    /// <summary>
    /// Transitions from the home screen to the requests screen.
    /// </summary>
    public void HomeToRequests()
    {
        NavigationController.HomeToRequests();
    }

    /// <summary>
    /// Transitions from the home screen to the reply screen.
    /// </summary>
    public void HomeToReply()
    {
        NavigationController.HomeToReply();
    }

    /// <summary>
    /// Transitions from the home screen to the customization screen.
    /// </summary>
    public void HomeToCustomization()
    {
        NavigationController.HomeToCustomization();
    }

    /// <summary>
    /// Destroys the request buttons from the requests screen.
    /// </summary>
    public void DestroyRequestButtons()
    {
        UIRequest.DestroyRequestButtons();
    }

    /// <summary>
    /// Destroys the reqply buttons from the reply screen.
    /// </summary>
    public void DestroyReplyButtons()
    {
        UIReply.DestroyReplyButtons();
    }

    /// <summary>
    /// Destroys the mail gameobjects from the mailbox screen.
    /// </summary>
    public void DestroyMailboxGameObjects()
    {
        UIMailbox.DestroyMailboxGameObjects();
    }
}
