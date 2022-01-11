using UnityEngine;

/// <summary>
/// Calls animations for UI screens and the respectful gameObjects if necessary.
/// </summary>
public class NavigationController : MonoBehaviour
{
    [SerializeField]
    private Animator uiAnimator;
    [SerializeField]
    private Animator uiCompanionActionsAnimator;
    [SerializeField]
    private Animator diaryAnimator;
    [SerializeField]
    private Animator companionAnimator;
    [SerializeField]
    private Animator stickerBookAnimator;
    [SerializeField]
    private Animator giftAnimator;

    [SerializeField]
    private GameObject canvasCompanionActions;
    [SerializeField]
    private Canvas canvas;

    #region Screen Navigation

    /// <summary>
    /// Transitions from the home screen to the diary screen.
    /// </summary>
    public void HomeToDiary()
    {
        string animationStateName = "Navigation - Home to Diary";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
        diaryAnimator.Play(animationStateName);
        stickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the diary screen to the home screen.
    /// </summary>
    public void DiaryToHome()
    {
        string animationStateName = "Navigation - Diary to Home";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
        diaryAnimator.Play(animationStateName);
        stickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the home screen to the requests screen.
    /// </summary>
    public void HomeToRequests()
    {
        string animationStateName = "Navigation - Home to Requests";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transition from the home screen to the stickers screen.
    /// </summary>
    public void HomeToSticker()
    {
        string animationStateName = "Navigation - Home to Sticker";
        SetRenderMode(RenderMode.ScreenSpaceCamera);
        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
        stickerBookAnimator.Play(animationStateName);
    }

    public void StickerToHome()
    {
        string animationStateName = "Navigation - Sticker to Home";
        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
        stickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transition from the requests screen to the home screen.
    /// </summary>
    public void RequestsToHome()
    {
        string animationStateName = "Navigation - Requests to Home";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the home screen to the mailbox screen.
    /// </summary>
    public void HomeToMailbox()
    {
        string animationStateName = "Navigation - Home to Mailbox";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the home screen.
    /// </summary>
    public void MailboxToHome()
    {
        string animationStateName = "Navigation - Mailbox to Home";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the home screen.
    /// </summary>
    public void ReplyToHome()
    {
        string animationStateName = "Navigation - Reply to Home";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the home screen.
    /// </summary>
    public void HomeToReply()
    {
        string animationStateName = "Navigation - Home to Reply";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    /// <param name="giftIncluded">True to play gift animation.</param>
    public void MailboxToMail(bool giftIncluded)
    {
        string animationStateName = "Navigation - Mailbox to Mail";

        uiAnimator.Play(animationStateName);
        if (giftIncluded)
        {
            giftAnimator.Play(animationStateName);
        }
    }

    /// <summary>
    /// Transitions from the home screen to the customization screen.
    /// </summary>
    public void HomeToCustomization()
    {
        string animationStateName = "Navigation - Home to Customization";

        uiAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the customization screen to the home screen.
    /// </summary>
    public void CustomizationToHome()
    {
        string animationStateName = "Navigation - Customization to Home";

        uiAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the home screen to the settings screen.
    /// </summary>
    public void HomeToSettings()
    {
        string animationStateName = "Navigation - Home to Settings";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the settings screen to the home screen.
    /// </summary>
    public void SettingsToHome()
    {
        string animationStateName = "Navigation - Settings to Home";

        uiAnimator.Play(animationStateName);
        companionAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the mail screen to the mailbox screen.
    /// </summary>
    /// <param name="giftIncluded">True to play gift animation.</param>
    public void MailToMailbox(bool giftIncluded)
    {
        string animationStateName = "Navigation - Mail to Mailbox";

        uiAnimator.Play(animationStateName);
        if (giftIncluded)
        {
            giftAnimator.Play(animationStateName);
        }
    }

    /// <summary>
    /// Transitions from the reply screen to the gift screen.
    /// </summary>
    public void ReplyToGift()
    {
        string animationStateName = "Navigation - Reply to Gift";

        uiAnimator.Play(animationStateName);
        giftAnimator.Play(animationStateName);
        stickerBookAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transitions from the gift screen to the reply screen.
    /// </summary>
    public void GiftToReply()
    {
        string animationStateName = "Navigation - Gift to Reply";

        uiAnimator.Play(animationStateName);
        giftAnimator.Play(animationStateName);
        stickerBookAnimator.Play(animationStateName);
    }

    public void GiftToHome()
    {
        string animationStateName = "Navigation - Gift to Home";
        string animationStateName2 = "Navigation - Gift to Reply";

        uiAnimator.Play(animationStateName);
        giftAnimator.Play(animationStateName2);
        stickerBookAnimator.Play(animationStateName2);
    }

    /// <summary>
    /// Transitions from the home screen (companion actions) to the requests screen.
    /// </summary>
    public void CompanionActionsToRequests()
    {
        uiCompanionActionsAnimator.Play("Fade Out Companion Actions To Requests");
    }

    /// <summary>
    /// Transitions from the home screen (companion actions) to the reply screen.
    /// </summary>
    public void CompanionActionsToReply()
    {
        uiCompanionActionsAnimator.Play("Fade Out Companion Actions To Reply");
    }

    /// <summary>
    /// Transitions from the home screen (companion actions) to the customization screen.
    /// </summary>
    public void CompanionActionsToCustomization()
    {
        uiCompanionActionsAnimator.Play("Fade Out Companion Actions To Customization");
    }

    #endregion

    /// <summary>
    /// Shows or hides companion action dialogue choices.
    /// </summary>
    /// <remarks>Setting the canvas inactive is done in <see cref="CompanionActionAnimationHelper.DeactivateCompanionActions"/></remarks>
    public void ToggleCompanionActions()
    {
        bool showing = !canvasCompanionActions.activeSelf;
        if (showing)
        {
            canvasCompanionActions.SetActive(true);
            SetRenderMode(RenderMode.ScreenSpaceCamera);
        }
        uiCompanionActionsAnimator.Play("Fade " + (showing ? "In" : "Out") + " Companion Actions");
    }

    /// <summary>
    /// Sets the rendermode of the UI.
    /// </summary>
    /// <param name="renderMode">The render mode to use.</param>
    public void SetRenderMode(RenderMode renderMode)
    {
        canvas.renderMode = renderMode;
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
