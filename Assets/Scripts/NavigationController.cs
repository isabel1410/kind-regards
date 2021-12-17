using UnityEngine;

public class NavigationController : MonoBehaviour
{
    public Animator UIAnimator;
    public Animator UICompanionActionsAnimator;
    public Animator DiaryAnimator;
    public Animator OwlAnimator;
    public Animator StickerAnimator;

    public GameObject CanvasCompanionActions;

    #region Screen Navigation

    /// <summary>
    /// Transition from the home screen to the diary screen.
    /// </summary>
    public void HomeToDiary()
    {
        string animationStateName = "Navigation - Home to Diary";

        UIAnimator.Play(animationStateName);
        OwlAnimator.Play(animationStateName);
        DiaryAnimator.Play(animationStateName);
        StickerAnimator.Play(animationStateName);
    }

    /// <summary>
    /// Transition from the diary screen to the home screen.
    /// </summary>
    public void DiaryToHome()
    {
        string animationStateName = "Navigation - Diary to Home";

        UIAnimator.Play(animationStateName);
        OwlAnimator.Play(animationStateName);
        DiaryAnimator.Play(animationStateName);
        StickerAnimator.Play(animationStateName);
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
}
