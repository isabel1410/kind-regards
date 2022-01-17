using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Holds a list of <see cref="DiaryEntry"/>, saves and loads them.
/// </summary>
public class Diary : MonoBehaviour
{
    private string DIARYDIRECTORYPATH => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Diary";

    [SerializeField]
    private List<DiaryEntry> entries;
    private DiaryEntry currentEntry;

    [SerializeField]
    private UIDiary uiDiary;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private NavigationController navigationController;

    #region File Management

    /// <summary>
    /// Load all diaries and shows the <see cref="DiaryEntry"/> of today.
    /// </summary>
    /// <returns>True if loading was succesful.</returns>
    public bool Load(out DiaryEntry[] diaryEntries)
    {
        entries = new List<DiaryEntry>();

        try
        {
            //Check if path exists
            if (!Directory.Exists(DIARYDIRECTORYPATH))
            {
                Directory.CreateDirectory(DIARYDIRECTORYPATH);
            }

            //Load and sort existing entries
            else
            {
                foreach (string entryPath in Directory.GetFiles(DIARYDIRECTORYPATH))
                {
                    entries.Add(new DiaryEntry());
                    string entryData = File.ReadAllText(entryPath);
                    JsonUtility.FromJsonOverwrite(entryData, entries[entries.Count - 1]);
                }
                entries.Sort();
            }
            diaryEntries = entries.ToArray();
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Loading diary entries failed");
            diaryEntries = null;
            return false;
        }

        //Show entry of today (creates one if needed)
        if (entries.Count == 0 || entries[entries.Count - 1].Date != DateTime.Today)
        {
            CreateNew();
        }
        ShowEntry(entries[entries.Count - 1]);

        return true;
    }

    public DiaryEntry[] GetDiaryEntries()
    {
        Load(out DiaryEntry[] entries);
        return entries;
    }

    /// <summary>
    /// Saves <see cref="currentEntry"/> if needed, deletes it when needed.
    /// </summary>
    /// <returns>True if saving was succesful.</returns>
    public bool SaveCurrentEntry()
    {
        try
        {
            string entryPath = $"{DIARYDIRECTORYPATH}{Path.DirectorySeparatorChar}{currentEntry.Date:dd-MM-yyyy}.json";
            SetEntry(uiDiary.GetEntry());

            //Empty diary, delete if existing
            if (currentEntry.Mood == DiaryEntry.DiaryMood.Empty && string.IsNullOrWhiteSpace(currentEntry.Entry))
            {
                if (File.Exists(entryPath))
                {
                    return DeleteCurrentEntry();
                }
                else
                {
                    Debug.Log("Diary entry not saved");
                    return true;
                }
            }

            //Save to file
            string entry = JsonUtility.ToJson(currentEntry);
            File.WriteAllText(entryPath, entry);
            Debug.Log("Diary entry saved");
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Saving diary entry failed");
            return false;
        }
    }

    /// <summary>
    /// Deletes <see cref="currentEntry"/> in the files and shows the next <see cref="DiaryEntry"/>.
    /// </summary>
    /// <returns>True if the entry is succesfully deleted, false otherwise.</returns>
    public bool DeleteCurrentEntry()
    {
        try
        {
            string entryPath = $"{DIARYDIRECTORYPATH}{Path.DirectorySeparatorChar}{currentEntry.Date:dd-MM-yyyy}.json";
            File.Delete(entryPath);
            Debug.Log("Diary entry deleted");

            int index = entries.IndexOf(currentEntry);
            //If the entry is from today, there is no point in deleting it, so the player can still add on for today
            if (currentEntry.Date != DateTime.Today)
            {
                entries.RemoveAt(index);
            }

            ShowEntry(entries[index]);
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Deleting diary entry failed");
            return false;
        }
    }

    #endregion

    #region Diary Specific

    /// <summary>
    /// Creates a new <see cref="DiaryEntry"/>.
    /// </summary>
    private void CreateNew()
    {
        DiaryEntry entry = new DiaryEntry()
        {
            Date = DateTime.Today
        };
        entries.Add(entry);
        currentEntry = entries[entries.Count - 1];
    }

    /// <summary>
    /// Sets the entry text for <see cref="currentEntry"/>.
    /// </summary>
    /// <param name="text">The text to put in the entry.</param>
    private void SetEntry(string text)
    {
        currentEntry.Entry = text;
    }

    /// <summary>
    /// Sets the mood of <see cref="currentEntry"/> and updates the UI.
    /// </summary>
    /// <param name="moodIndex"><see cref="DiaryEntry.DiaryMood"/> represented by an <see cref="int"/></param>
    public void SetMood(int moodIndex)
    {
        //Get mood (empty if the mood is unselected)
        DiaryEntry.DiaryMood mood = (DiaryEntry.DiaryMood)moodIndex;
        DiaryEntry.DiaryMood UIMood = uiDiary.GetMood();
        if (mood == UIMood)
        {
            mood = DiaryEntry.DiaryMood.Empty;
        }

        currentEntry.Mood = mood;
        uiDiary.SetMood(mood);
    }

    /// <summary>
    /// Add a sticker.
    /// </summary>
    /// <exception cref="NotImplementedException">This function is not yet implemented.</exception>
    public void AddSticker(DataSticker sticker)
    {
        if (sticker == null)
        {
            currentEntry.StickerId = 0;
            return;
        }
        currentEntry.StickerId = sticker.Id;
    }

    #endregion

    #region Visuals

    /// <summary>
    /// Saves <see cref="currentEntry"/> and loads the next entry.
    /// </summary>
    public void NextPage()
    {
        if (!SaveCurrentEntry())
        {
            return;
        }

        int index = entries.IndexOf(currentEntry) + 1;
        ShowEntry(entries[index]);
    }

    /// <summary>
    /// Saves <see cref="currentEntry"/> and loads the previous entry.
    /// </summary>
    public void PreviousPage()
    {
        if (!SaveCurrentEntry())
        {
            return;
        }

        ShowEntry(entries[entries.IndexOf(currentEntry) - 1]);
    }

    /// <summary>
    /// Sets <see cref="currentEntry"/> and updates the UI.
    /// </summary>
    /// <param name="entry">The entry to show.</param>
    private void ShowEntry(DiaryEntry entry)
    {
        currentEntry = entry;

        uiDiary.SetEntry(entry, entries.IndexOf(entry) == 0, entry.Date == DateTime.Today);
    }

    /// <summary>
    /// Loads diary and transitions from the home screen to the diary screen.
    /// </summary>
    public void Show()
    {
        if (Load(out _))
        {
            navigationController.HomeToDiary();
        }
    }

    /// <summary>
    /// Saves <see cref="currentEntry"/> and exits to the home screen.
    /// </summary>
    public void Exit()
    {
        if (SaveCurrentEntry())
        {
           navigationController.DiaryToHome();
        }
    }

    #endregion

    /// <summary>
    /// Entry for a diary.
    /// </summary>
    [Serializable]
    public class DiaryEntry : IComparable<DiaryEntry>
    {
        [SerializeField]
        private string date;
        public DiaryMood Mood;
        public string Entry;
        public int StickerId;

        public DateTime Date
        {
            get
            {
                return DateTime.Parse(date);
            }
            set
            {
                date = value.ToShortDateString();
            }
        }

        /// <summary>
        /// For sorting purposes. Sorts on <see cref="Date"/>.
        /// </summary>
        /// <param name="other">The entry to compare this entry to.</param>
        /// <returns>-1 when <paramref name="other"/> is earlier, 0 when it is the same day, 1 when it is later</returns>
        public int CompareTo(DiaryEntry other)
        {
            return Date.CompareTo(other.Date);
        }

        /// <summary>
        /// Used to specify the mood of the diary.
        /// </summary>
        public enum DiaryMood : sbyte
        {
            Negative = -1,
            Empty = 0,
            Neutral = 1,
            Positive = 2
        }
    }
}
