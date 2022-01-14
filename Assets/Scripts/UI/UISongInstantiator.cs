using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to assign variables to the UI of the song in the settings screen.
/// </summary>
public class UISongInstantiator : MonoBehaviour
{
    [SerializeField]
    private Song song;
    [SerializeField]
    private Toggle uiIncluded;
    [SerializeField]
    private Text uiName;
    [SerializeField]
    private Text uiDuration;

    /// <summary>
    /// Assigns variables and updates the UI.
    /// </summary>
    /// <param name="dataSong">Song to show.</param>
    /// <param name="musicPlayer">Music player the song belongs to.</param>
    public void Instantiate(DataSong dataSong, MusicPlayer musicPlayer)
    {
        song.Loading = true;
        song.MusicPlayer = musicPlayer;
        song.DataSong = dataSong;
        uiIncluded.isOn = dataSong.Included;
        uiName.text = dataSong.Name;
        uiDuration.text = $"{(uint)dataSong.AudioClip.length / 60}:{(uint)dataSong.AudioClip.length % 60:00}";
        song.Loading = false;

        Destroy(this);
    }
}
