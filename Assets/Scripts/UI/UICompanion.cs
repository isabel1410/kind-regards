using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains all functions related to <see cref="Companion"/> for the UI.
/// </summary>
public class UICompanion : MonoBehaviour
{
    [SerializeField]
    private Text uiSpeechBubble;

    private IEnumerator dialogueRunner;
    private string owlActionsQuestion;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        owlActionsQuestion = uiSpeechBubble.text;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Displays a message character for character.
    /// </summary>
    /// <param name="message">Message to display.</param>
    public void Speak(string message)
    {
        if (dialogueRunner != null)
        {
            StopCoroutine(dialogueRunner);
        }
        dialogueRunner = RunDialogue(message);
        StartCoroutine(dialogueRunner);
    }

    /// <summary>
    /// Used to display a message character for character.
    /// </summary>
    /// <param name="message">Message to display.</param>
    /// <returns></returns>
    private IEnumerator RunDialogue(string message)
    {
        uiSpeechBubble.text = string.Empty;
        foreach (char character in message)
        {
            uiSpeechBubble.text += character;
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

    /// <summary>
    /// Resets the text of the owl. Used when the player exits and enters the companion actions screen.
    /// </summary>
    public void ResetText()
    {
        uiSpeechBubble.text = owlActionsQuestion;
    }
}
