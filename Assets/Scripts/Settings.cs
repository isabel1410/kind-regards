using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Saves and loads settings. For now only includes settings for <see cref="MusicPlayer"/>.
/// </summary>
public class Settings : MonoBehaviour
{
    private string SETTINGSPATH => $"{Application.persistentDataPath}{Path.DirectorySeparatorChar}Settings.json";

    [SerializeField]
    private DataSettings dataSettings;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private MusicPlayer musicPlayer;

    #region File Management

    /// <summary>
    /// Loads <see cref="DataSettings"/> from the local file and applies it to <see cref="musicPlayer"/>. If the settings do not exist, it will create them.
    /// </summary>
    /// <returns>True if loading succeeded.</returns>
    public bool Load()
    {
        try
        {
            if (!File.Exists(SETTINGSPATH))
            {
                musicPlayer.ApplyDefaultSettings(ref dataSettings);
            }
            else
            {
                JsonUtility.FromJsonOverwrite(File.ReadAllText(SETTINGSPATH), dataSettings);
            }
            musicPlayer.ApplySettings(dataSettings);
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Settings could not be loaded");
            return false;
        }
    }

    /// <summary>
    /// Saves <see cref="DataSettings"/> to a local file.
    /// </summary>
    /// <returns>True if saving succeeded.</returns>
    private bool Save()
    {
        try
        {
            File.WriteAllText(SETTINGSPATH, JsonUtility.ToJson(dataSettings));
            return true;
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Settings could not be saved");
            return false;
        }
    }

    #endregion

    #region Visuals

    /// <summary>
    /// Updates the UIs and transitions to the settings screen.
    /// </summary>
    public void Show()
    {
        musicPlayer.Show();
        navigationController.HomeToSettings();
    }

    /// <summary>
    /// Saves <see cref="dataSettings"/> and transitions to the home screen.
    /// </summary>
    public void Exit()
    {
        if (Save())
        {
            navigationController.SettingsToHome();
        }
    }

    #endregion

    #region MusicPlayer

    /// <summary>
    /// Sets the shuffle option from <see cref="musicPlayer"/>.
    /// </summary>
    /// <param name="shuffled">If the music is shuffled.</param>
    public void SetShuffle(bool shuffled)
    {
        dataSettings.MusicShuffle = shuffled;
    }

    /// <summary>
    /// Sets the volume option from <see cref="musicPlayer"/>.
    /// </summary>
    /// <param name="volume">Volume of the music.</param>
    public void SetVolume(float volume)
    {
        dataSettings.MusicVolume = volume;
    }

    /// <summary>
    /// Sets the inclusion value of a song from <see cref="musicPlayer"/>.
    /// </summary>
    /// <param name="index">Index of the song in <see cref="DataSettings.MusicEnabled"/>.</param>
    /// <param name="included">If the song should be included.</param>
    public void SetInclusion(int index, bool included)
    {
        dataSettings.MusicEnabled[index] = included;
    }

    #endregion
}
