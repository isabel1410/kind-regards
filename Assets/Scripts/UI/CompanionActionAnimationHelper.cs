using UnityEngine;

/// <summary>
/// The only purpose is to set the canvas inactive once the companion action dialogue choices are hidden.
/// </summary>
public class CompanionActionAnimationHelper : MonoBehaviour
{
    [SerializeField]
    private NavigationController NavigationController;
    [SerializeField]
    private Request Request;
    [SerializeField]
    private Reply Reply;
    [SerializeField]
    private Companion Companion;

    /// <summary>
    /// Sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void DeactivateCompanionActions()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Activates transition to requests screen and sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void CompanionActionsToRequests()
    {
        Request.Show();
        DeactivateCompanionActions();
    }

    /// <summary>
    /// Activates transition to reply screen and sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void CompanionActionsToReply()
    {
        Reply.Show();
        DeactivateCompanionActions();
    }

    /// <summary>
    /// Activates transition to customization screen and sets the canvas inactive once the companion action dialogue choices are hidden.
    /// </summary>
    public void CompanionActionsToCustomization()
    {
        Companion.ShowCustomization();
        DeactivateCompanionActions();
    }
}
