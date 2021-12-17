using UnityEngine;
using UnityEngine.UI;

public class UIDiary : MonoBehaviour
{
    public Text UIDate;
    public InputField UIEntry;
    public Button UIMoodNegative;
    public Button UIMoodNeutral;
    public Button UIMoodPositive;
    public Button UIPrevious;
    public Button UINext;

    /// <summary>
    /// Update the UI to show a <see cref="Diary.DiaryEntry"/>.
    /// </summary>
    /// <param name="entry">The <see cref="Diary.DiaryEntry"/> to show.</param>
    /// <param name="first">True if this is the earliest entry.</param>
    /// <param name="last">True if this is the latest entry.</param>
    public void SetEntry(Diary.DiaryEntry entry, bool first, bool last)
    {
        //Load info
        UIDate.text = entry.Date.ToShortDateString();
        UIEntry.text = entry.Entry;
        SetMood(entry.Mood);

        //Previous / next button disabling
        if (first)
        {
            UIPrevious.gameObject.SetActive(false);
        }
        else
        {
            UIPrevious.gameObject.SetActive(true);
        }
        if (last)
        {
            UINext.gameObject.SetActive(false);
        }
        else
        {
            UINext.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Update the buttons to show the selected <see cref="Diary.DiaryEntry.DiaryMood"/>.
    /// </summary>
    /// <param name="mood">The mood to show.</param>
    public void SetMood(Diary.DiaryEntry.DiaryMood mood)
    {
        //Reset all colors
        UIMoodNegative.GetComponent<Image>().color = Color.white;
        UIMoodNeutral.GetComponent<Image>().color = Color.white;
        UIMoodPositive.GetComponent<Image>().color = Color.white;

        //Give color if necessary (not in case of Diary.DiaryEntry.DiaryMood.Empty)
        switch (mood)
        {
            case Diary.DiaryEntry.DiaryMood.Negative:
                UIMoodNegative.GetComponent<Image>().color = Color.red;
                break;
            case Diary.DiaryEntry.DiaryMood.Neutral:
                UIMoodNeutral.GetComponent<Image>().color = Color.yellow;
                break;
            case Diary.DiaryEntry.DiaryMood.Positive:
                UIMoodPositive.GetComponent<Image>().color = Color.green;
                break;
        }
    }

    /// <summary>
    /// Get the text of the current <see cref="Diary.DiaryEntry.Entry"/>.
    /// </summary>
    /// <returns>Text of the entry.</returns>
    public string GetEntry()
    {
        return UIEntry.text;
    }

    /// <summary>
    /// Get the mood of the current <see cref="Diary.DiaryEntry.Mood"/>.
    /// </summary>
    /// <returns>Mood of the entry.</returns>
    public Diary.DiaryEntry.DiaryMood GetMood()
    {
        if (UIMoodNegative.GetComponent<Image>().color != Color.white)
        {
            return Diary.DiaryEntry.DiaryMood.Negative;
        }
        if (UIMoodNeutral.GetComponent<Image>().color != Color.white)
        {
            return Diary.DiaryEntry.DiaryMood.Neutral;
        }
        if (UIMoodPositive.GetComponent<Image>().color != Color.white)
        {
            return Diary.DiaryEntry.DiaryMood.Positive;
        }
        return Diary.DiaryEntry.DiaryMood.Empty;
    }
}
