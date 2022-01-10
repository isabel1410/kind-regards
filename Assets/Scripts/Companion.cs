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
    private UICustomization uiCustomization;
    [SerializeField]
    private UICompanion uiCompanion;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private Diary diary;
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private DataCustomization dataCustomization = new DataCustomization() { Color = Color.red };
    public bool HasReturned;

    private string CUSTOMIZATIONPATH => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Customization.json";

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        LoadCustomization();
        uiCustomization.ApplyCustomization(dataCustomization);
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
            dataCustomization = JsonConvert.DeserializeObject<DataCustomization>(File.ReadAllText(CUSTOMIZATIONPATH));
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Loading customization failed");
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
            File.WriteAllText(CUSTOMIZATIONPATH, JsonConvert.SerializeObject(dataCustomization, new ColorConverter()));
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Saving customization failed");
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
        dataCustomization.Color = color;
        uiCustomization.ChangeColor(color);
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
                DiaryEntry[] entries = diary.GetDiaryEntries();
                reply = GetMoodMessage(entries);
                break;
        }

        uiCompanion.Speak(reply);
    }

    /// <summary>
    /// Toggles companion actions screen and resets the speechbubble of the companion.
    /// </summary>
    public void ToggleCompanionActions()
    {
        uiCompanion.ResetText();
        navigationController.ToggleCompanionActions();
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
        navigationController.HomeToCustomization();
    }

    /// <summary>
    /// Saves the customization and exits to the home screen.
    /// </summary>
    public void ExitCustomization()
    {
        if (SaveCustomization())
        {
            navigationController.CustomizationToHome();
        }
    }

    public void FlyAway()
    {
        HasReturned = false;
        GetComponent<Animator>().Play("FlyingTo");
    }

    public void FlyBack()
    {
        GetComponent<Animator>().Play("FlyingBack");
    }

    public void OnArrivedBack()
    {
        HasReturned = true;
    }

    #endregion
}
