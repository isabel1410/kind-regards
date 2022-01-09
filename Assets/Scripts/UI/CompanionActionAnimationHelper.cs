using UnityEngine;

/// <summary>
/// The only purpose is to set the canvas inactive once the companion action dialogue choices are hidden.
/// </summary>
public class CompanionActionAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private Request request;
    [SerializeField]
    private Reply reply;
    [SerializeField]
    private Companion companion;
    [SerializeField]
    private NavigationController navigationController;

    /// <summary>
    /// Sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void DeactivateCompanionActions()
    {
        navigationController.SetRenderMode(RenderMode.ScreenSpaceOverlay);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Activates transition to requests screen and sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void CompanionActionsToRequests()
    {
        request.Show();
        DeactivateCompanionActions();
    }

    /// <summary>
    /// Activates transition to reply screen and sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void CompanionActionsToReply()
    {
        reply.Show();
        DeactivateCompanionActions();
    }

    /// <summary>
    /// Activates transition to customization screen and sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void CompanionActionsToCustomization()
    {
        companion.ShowCustomization();
        DeactivateCompanionActions();
    }
}
