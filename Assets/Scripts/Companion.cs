using UnityEngine;
using static Diary;

/// <summary>
/// Used for interactions with the companion.
/// </summary>
public class Companion : MonoBehaviour
{
    public UICompanion UICompanion;
    public Diary Diary;
    public NavigationController NavigationController;

    /// <summary>
    /// Lets the companion display a message.
    /// </summary>
    public void Talk()
    {
        string reply;

        float randomValue = Random.value;
        switch (randomValue)
        {
            default:
                reply = GetMessage();
                break;

            //Mood
            case float _ when randomValue < .5:
                DiaryEntry[] entries = Diary.GetDiaryEntries();
                reply = GetMoodMessage(entries);
                break;
        }

        UICompanion.Speak(reply);
    }

    /// <summary>
    /// Get a random message.
    /// </summary>
    /// <returns>Message.</returns>
    private string GetMessage()
    {
        string[] replies = GameObject.Find("TEMP").GetComponent<TEMP>().GetCompanionMessages();
        return replies[Random.Range(0, replies.Length)];
    }

    /// <summary>
    /// Get a random message based on the mood of the last diary.
    /// </summary>
    /// <example><see cref="DiaryEntry.DiaryMood.Negative"/> results in "I hope your day gets better".</example>
    /// <param name="entries">Diary entries.</param>
    /// <returns>Message.</returns>
    private string GetMoodMessage(DiaryEntry[] entries)
    {
        TEMP TEMP = GameObject.Find("TEMP").GetComponent<TEMP>();
        string[] messages;
        switch (entries[entries.Length - 1].Mood)
        {
            default://DiaryEntry.DiaryMood.Neutral or DiaryEntry.DiaryMood.Empty
                messages = TEMP.GetCompanionMessages();
                break;
            case DiaryEntry.DiaryMood.Negative:
                messages = TEMP.GetCompanionMessagesNegative();
                break;
            case DiaryEntry.DiaryMood.Positive:
                messages = TEMP.GetCompanionMessagesPositive();
                break;
        }
        return messages[Random.Range(0, messages.Length)];
    }

    /// <summary>
    /// Toggles companion actions screen and resets the speechbubble of the companion.
    /// </summary>
    public void ToggleCompanionActions()
    {
        UICompanion.ResetText();
        NavigationController.ToggleCompanionActions();
    }
}
