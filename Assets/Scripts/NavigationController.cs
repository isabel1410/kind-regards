using UnityEngine;

/// <summary>
/// Calls animations for UI screens and the respectful gameObjects if necessary.
/// </summary>
public class NavigationController : MonoBehaviour
{
    public Animator UIAnimator;
    public Animator UICompanionActionsAnimator;
    public Animator DiaryAnimator;
    public Animator CompanionAnimator;
    public Animator StickerBookAnimator;
    public Animator GiftAnimator;

    public GameObject CanvasCompanionActions;
    public Canvas Canvas;

    #region Screen Navigation

    /// <summary>
    /// Transitions from the home screen to the diary screen.
    /// </summary>
    public void HomeToDiary()
    {
        string animationStateName = "Navigation - Home to Diary";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
        DiaryAnimator.Play(animationStateName);
        StickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the diary screen to the home screen.
    /// </summary>
    public void DiaryToHome()
    {
        string animationStateName = "Navigation - Diary to Home";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
        DiaryAnimator.Play(animationStateName);
        StickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the home screen to the requests screen.
    /// </summary>
    public void HomeToRequests()
    {
        string animationStateName = "Navigation - Home to Requests";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the requests screen to the home screen.
    /// </summary>
    public void RequestsToHome()
    {
        string animationStateName = "Navigation - Requests to Home";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the home screen to the mailbox screen.
    /// </summary>
    public void HomeToMailbox()
    {
        string animationStateName = "Navigation - Home to Mailbox";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the home screen.
    /// </summary>
    public void MailboxToHome()
    {
        string animationStateName = "Navigation - Mailbox to Home";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the home screen.
    /// </summary>
    public void ReplyToHome()
    {
        string animationStateName = "Navigation - Reply to Home";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the home screen.
    /// </summary>
    public void HomeToReply()
    {
        string animationStateName = "Navigation - Home to Reply";

        UIAnimator.Play(animationStateName);
        CompanionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    /// <param name="giftIncluded">True to play gift animation.</param>
    public void MailboxToMail(bool giftIncluded)
    {
        string animationStateName = "Navigation - Mailbox to Mail";

        UIAnimator.Play(animationStateName);
        if (giftIncluded)
        {
            GiftAnimator.Play(animationStateName);
        }
    }

    /// <summary>
    /// Transitions from the home screen to the customization screen.
    /// </summary>
    public void HomeToCustomization()
    {
        string animationStateName = "Navigation - Home to Customization";

        UIAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the customization screen to the home screen.
    /// </summary>
    public void CustomizationToHome()
    {
        string animationStateName = "Navigation - Customization to Home";

        UIAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mail screen to the mailbox screen.
    /// </summary>
    /// <param name="giftIncluded">True to play gift animation.</param>
    public void MailToMailbox(bool giftIncluded)
    {
        string animationStateName = "Navigation - Mail to Mailbox";

        UIAnimator.Play(animationStateName);
        if (giftIncluded)
        {
            GiftAnimator.Play(animationStateName);
        }
    }

    /// <summary>
    /// Transitions from the reply screen to the gift screen.
    /// </summary>
    public void ReplyToGift()
    {
        string animationStateName = "Navigation - Reply to Gift";

        UIAnimator.Play(animationStateName);
        GiftAnimator.Play(animationStateName);
        StickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the gift screen to the reply screen.
    /// </summary>
    public void GiftToReply()
    {
        string animationStateName = "Navigation - Gift to Reply";

        UIAnimator.Play(animationStateName);
        GiftAnimator.Play(animationStateName);
        StickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the home screen (companion actions) to the requests screen.
    /// </summary>
    public void CompanionActionsToRequests()
    {
        UICompanionActionsAnimator.Play("Fade Out Companion Actions To Requests");
    }

    /// <summary>
    /// Transitions from the home screen (companion actions) to the reply screen.
    /// </summary>
    public void CompanionActionsToReply()
    {
        UICompanionActionsAnimator.Play("Fade Out Companion Actions To Reply");
    }

    /// <summary>
    /// Transitions from the home screen (companion actions) to the customization screen.
    /// </summary>
    public void CompanionActionsToCustomization()
    {
        UICompanionActionsAnimator.Play("Fade Out Companion Actions To Customization");
    }

    #endregion

    /// <summary>
    /// Shows or hides companion action dialogue choices.
    /// </summary>
    /// <remarks>Setting the canvas inactive is done in <see cref="CompanionActionDialogueDeactivator.DeactivateCompanionActions"/></remarks>
    public void ToggleCompanionActions()
    {
        bool showing = !CanvasCompanionActions.activeSelf;
        if (showing)
        {
            CanvasCompanionActions.SetActive(true);
        }
        UICompanionActionsAnimator.Play("Fade " + (showing ? "In" : "Out") + " Companion Actions");
    }

    /// <summary>
    /// Sets the rendermode of the UI.
    /// </summary>
    /// <param name="renderMode">The render mode to use.</param>
    public void SetRenderMode(RenderMode renderMode)
    {
        Canvas.renderMode = renderMode;
    }

    /// <summary>
    /// Shows <see cref="UIError"/>.
    /// </summary>
    /// <param name="canvasErrorAnimator"><see cref="Animator"/> component of <see cref="UIError"/>.</param>
    public void ShowError(Animator canvasErrorAnimator)
    {
        canvasErrorAnimator.Play("Show Error");
    }

    /// <summary>
    /// Exits out of <see cref="UIError"/>.
    /// </summary>
    /// <param name="canvasErrorAnimator"><see cref="Animator"/> component of <see cref="UIError"/>.</param>
    public void ExitError(Animator canvasErrorAnimator)
    {
        canvasErrorAnimator.Play("Exit Error");
    }
}
