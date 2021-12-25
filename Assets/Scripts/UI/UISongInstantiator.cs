using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Used to assign variables to the UI of the song in the settings screen.
/// </summary>
public class UISongInstantiator : MonoBehaviour
{
    [SerializeField]
    private Song Song;
    [SerializeField]
    private Toggle UIIncluded;
    [SerializeField]
    private Text UIName;
    [SerializeField]
    private Text UIDuration;

    /// <summary>
    /// Assigns variables and updates the UI.
    /// </summary>
    /// <param name="dataSong">Song to show.</param>
    /// <param name="musicPlayer">Music player the song belongs to.</param>
    public void Instantiate(DataSong dataSong, MusicPlayer musicPlayer)
    {
        Song.Loading = true;
        Song.MusicPlayer = musicPlayer;
        Song.DataSong = dataSong;
        UIIncluded.isOn = dataSong.Included;
        UIName.text = dataSong.Name;
        UIDuration.text = dataSong.AudioClip.length.ToString("#0:00");//"##:##"
        Song.Loading = false;

        Destroy(this);
    }
}
