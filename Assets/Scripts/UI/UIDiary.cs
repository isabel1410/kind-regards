using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the diary screen.
/// </summary>
public class UIDiary : MonoBehaviour
{
    [SerializeField]
    private Text uiDate;
    [SerializeField]
    private InputField uiEntry;
    [SerializeField]
    private Button uiMoodNegative;
    [SerializeField]
    private Button uiMoodNeutral;
    [SerializeField]
    private Button uiMoodPositive;
    [SerializeField]
    private StickerDecorationSlot uiStickerSlot;
    [SerializeField]
    private Button uiPrevious;
    [SerializeField]
    private Button uiNext;

    /// <summary>
    /// Update the UI to show a <see cref="Diary.DiaryEntry"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Diary.DiaryEntry"/> to show.</param>
    /// <param name="first">True if this is the earliest entry.</param>
    /// <param name="last">True if this is the latest entry.</param>
    public void SetEntry(Diary.DiaryEntry entry, bool first, bool last)
    {
        //Load info
        uiDate.text = entry.Date.ToShortDateString();
        uiEntry.text = entry.Entry;
        if(APIManager.Instance) uiStickerSlot.SetSticker(APIManager.Instance.DataStickers.FirstOrDefault(s => s.Id == entry.StickerId));
        SetMood(entry.Mood);

        //Previous / next button disabling
        uiPrevious.gameObject.SetActive(!first);
        uiNext.gameObject.SetActive(!last);
    }

    /// <summary>
    /// Update the buttons to show the selected <see cref="Diary.DiaryEntry.DiaryMood"/>.
    /// </summary>
    /// <param name="mood">The mood to show.</param>
    public void SetMood(Diary.DiaryEntry.DiaryMood mood)
    {
        //Reset all colors
        uiMoodNegative.GetComponent<Image>().color = Color.gray;
        uiMoodNeutral.GetComponent<Image>().color = Color.gray;
        uiMoodPositive.GetComponent<Image>().color = Color.gray;

        //Give color if necessary (not in case of Diary.DiaryEntry.DiaryMood.Empty)
        switch (mood)
        {
            case Diary.DiaryEntry.DiaryMood.Negative:
                uiMoodNegative.GetComponent<Image>().color = Color.red;
                break;
            case Diary.DiaryEntry.DiaryMood.Neutral:
                uiMoodNeutral.GetComponent<Image>().color = Color.yellow;
                break;
            case Diary.DiaryEntry.DiaryMood.Positive:
                uiMoodPositive.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    /// <summary>
    /// Get the text of the current <see cref="Diary.DiaryEntry.Entry"/>.
    /// </summary>
    /// <returns>Text of the entry.</returns>
    public string GetEntry()
    {
        return uiEntry.text;
    }

    /// <summary>
    /// Get the mood of the current <see cref="Diary.DiaryEntry.Mood"/>.
    /// </summary>
    /// <returns>Mood of the entry.</returns>
    public Diary.DiaryEntry.DiaryMood GetMood()
    {
        if (uiMoodNegative.GetComponent<Image>().color != Color.gray)
        {
            return Diary.DiaryEntry.DiaryMood.Negative;
        }
        if (uiMoodNeutral.GetComponent<Image>().color != Color.gray)
        {
            return Diary.DiaryEntry.DiaryMood.Neutral;
        }
        if (uiMoodPositive.GetComponent<Image>().color != Color.gray)
        {
            return Diary.DiaryEntry.DiaryMood.Positive;
        }
        return Diary.DiaryEntry.DiaryMood.Empty;
    }
}
