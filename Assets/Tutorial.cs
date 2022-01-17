using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject[] uiObjects;
    [SerializeField]
    private Text uiStage;
    [SerializeField]
    private Text uiDialogue;
    [SerializeField]
    private Animator companionActionsAnimator;
    [SerializeField]
    private RuntimeAnimatorController navigationAnimatorController;
    [SerializeField]
    private RuntimeAnimatorController companionAnimatorController;
    [SerializeField]
    private RuntimeAnimatorController diaryAnimatorController;
    [SerializeField]
    private RuntimeAnimatorController stickerBookAnimatorController;
    [SerializeField]
    private RuntimeAnimatorController tutorialAnimatorController;
    [SerializeField]
    private Animator companionAnimator;
    [SerializeField]
    private Animator diaryAnimator;
    [SerializeField]
    private Animator stickerBookAnimator;

    private Animator canvasAnimator;
    private sbyte tutorialIndex;
    private IEnumerator dialogueRunner;

    private void Start()
    {
        canvasAnimator = GetComponent<Animator>();
    }

    [ContextMenu("Start tutorial")]
    public void StartTutorial()
    {
        companionActionsAnimator.enabled = false;
        canvasAnimator.runtimeAnimatorController = tutorialAnimatorController;
        companionAnimator.runtimeAnimatorController = tutorialAnimatorController;
        diaryAnimator.runtimeAnimatorController = tutorialAnimatorController;
        stickerBookAnimator.runtimeAnimatorController = tutorialAnimatorController;
        uiDialogue.text = string.Empty;
        tutorialIndex = 0;

        foreach (GameObject uiObject in uiObjects)
        {
            uiObject.SetActive(true);
        }
        Next();
    }

    public void Next()
    {
        if (dialogueRunner != null)
        {
            StopCoroutine(dialogueRunner);
        }
        uiDialogue.text = string.Empty;
        string animationStateName;
        switch (tutorialIndex++)
        {
            case 0:
                animationStateName = "00 - Welcome";
                break;
            case 1:
                animationStateName = "01 - Mailbox";
                break;
            case 2:
                animationStateName = "02 - Diary";
                break;
            case 3:
                animationStateName = "03 - StickerBook";
                diaryAnimator.WriteDefaultValues();
                diaryAnimator.enabled = false;
                break;
            case 4:
                animationStateName = "04 - CompanionActions";
                stickerBookAnimator.WriteDefaultValues();
                stickerBookAnimator.enabled = false;
                break;
            default:// Next is pressed
                OnFinishTutorial();
                return;
        };
        TryPlayAnimation(canvasAnimator, animationStateName);
        TryPlayAnimation(companionAnimator, animationStateName);
        TryPlayAnimation(diaryAnimator, animationStateName);
        TryPlayAnimation(stickerBookAnimator, animationStateName);
        uiStage.text = $"Tutorial\n{tutorialIndex}/5";
    }

    private void TryPlayAnimation(Animator animator, string animationStateName)
    {
        string layer;
        int layerId;
        switch (animator.gameObject.name)
        {
            default://Canvas
                layer = "UI Layer";
                layerId = 0;
                break;
            case "Owl":
                layer = "Companion Layer";
                layerId = 1;
                break;
            case "Diary Book":
                layer = "Diary Layer";
                layerId = 2;
                break;
            case "Sticker Book":
                layer = "StickerBook Layer";
                layerId = 3;
                break;
        }

        if (!animator.HasState(layerId, Animator.StringToHash(layer + "." + animationStateName)))
        {
            return;
        }

        animator.Play(layer + "." + animationStateName);
    }

    public void StartDialogue(string message)
    {
        if (dialogueRunner != null)
        {
            StopCoroutine(dialogueRunner);
        }
        dialogueRunner = RunDialogue(message);
        StartCoroutine(dialogueRunner);
    }

    public IEnumerator RunDialogue(string message)
    {
        uiDialogue.text = string.Empty;
        foreach (char character in message)
        {
            uiDialogue.text += character;
            WaitForSecondsRealtime time = character switch
            {
                '.' => new WaitForSecondsRealtime(.1f),
                char c when c == ',' || c == ':' || c == ';' => new WaitForSecondsRealtime(.075f),
                _ => new WaitForSecondsRealtime(0.05f),
            };
            yield return time;
        }
        dialogueRunner = null;
    }

    public void OnFinishTutorial()
    {
        foreach (GameObject uiObject in uiObjects)
        {
            uiObject.SetActive(false);
        }
        diaryAnimator.enabled = true;
        stickerBookAnimator.enabled = true;
        companionActionsAnimator.enabled = true;
        canvasAnimator.runtimeAnimatorController = navigationAnimatorController;
        companionAnimator.runtimeAnimatorController = companionAnimatorController;
        diaryAnimator.runtimeAnimatorController = diaryAnimatorController;
        stickerBookAnimator.runtimeAnimatorController = stickerBookAnimatorController;
    }
}
