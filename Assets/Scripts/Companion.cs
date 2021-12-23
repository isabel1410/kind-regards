using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using static Diary;

/// <summary>
/// Used for interactions with the companion.
/// </summary>
public class Companion : MonoBehaviour
{
    [SerializeField]
    private UICustomization UICustomization;
    [SerializeField]
    private UICompanion UICompanion;
    [SerializeField]
    private UIError UIError;
    [SerializeField]
    private Diary Diary;
    [SerializeField]
    private NavigationController NavigationController;
    [SerializeField]
    private DataCustomization DataCustomization = new DataCustomization() { Color = Color.red };

    private string CUSTOMIZATIONPATH => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Customization.json";

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        LoadCustomization();
        UICustomization.ApplyCustomization(DataCustomization);
    }

    #region File Management

    /// <summary>
    /// Loads the customization from a local file. (<see cref="CUSTOMIZATIONPATH"/>)
    /// </summary>
    /// <returns>True if the loading succeeded.</returns>
    public bool LoadCustomization()
    {
        try
        {
            if (!File.Exists(CUSTOMIZATIONPATH))
            {
                return SaveCustomization();
            }
            DataCustomization = JsonConvert.DeserializeObject<DataCustomization>(File.ReadAllText(CUSTOMIZATIONPATH));
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Loading customization failed");
            return false;
        }
    }

    /// <summary>
    /// Saves the customization to a local file. (<see cref="CUSTOMIZATIONPATH"/>)
    /// </summary>
    /// <returns>True if the saving succeeded.</returns>
    private bool SaveCustomization()
    {
        try
        {
            File.WriteAllText(CUSTOMIZATIONPATH, JsonConvert.SerializeObject(DataCustomization, new ColorConverter()));
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Saving customization failed");
            return false;
        }
    }

    #endregion

    #region Actions

    /// <summary>
    /// Changes the color of the companion.
    /// </summary>
    /// <param name="sender"></param>
    public void ChangeColor(GameObject sender)
    {
        Color color = sender.GetComponent<UnityEngine.UI.Image>().color;
        DataCustomization.Color = color;
        UICustomization.ChangeColor(color);
    }

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
    /// Toggles companion actions screen and resets the speechbubble of the companion.
    /// </summary>
    public void ToggleCompanionActions()
    {
        UICompanion.ResetText();
        NavigationController.ToggleCompanionActions();
    }

    #endregion

    #region Messages

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
        string[] messages = entries[entries.Length - 1].Mood switch
        {
            DiaryEntry.DiaryMood.Negative => TEMP.GetCompanionMessagesNegative(),
            DiaryEntry.DiaryMood.Positive => TEMP.GetCompanionMessagesPositive(),
            //DiaryEntry.DiaryMood.Neutral or DiaryEntry.DiaryMood.Empty
            _ => TEMP.GetCompanionMessages(),
        };
        return messages[Random.Range(0, messages.Length)];
    }

    #endregion

    #region Visuals

    /// <summary>
    /// Loads the customization and transitions from the home screen to the customization screen.
    /// </summary>
    public void ShowCustomization()
    {
        NavigationController.HomeToCustomization();
    }

    /// <summary>
    /// Saves the customization and exits to the home screen.
    /// </summary>
    public void ExitCustomization()
    {
        if (SaveCustomization())
        {
            NavigationController.CustomizationToHome();
        }
    }

    #endregion
}
