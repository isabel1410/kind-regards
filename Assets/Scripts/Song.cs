using UnityEngine;

/// <summary>
/// Used to toggle inclusion for a specific song.
/// </summary>
public class Song : MonoBehaviour
{
    [HideInInspector]
    public DataSong DataSong;
    [HideInInspector]
    public MusicPlayer MusicPlayer;
    [HideInInspector]
    public bool Loading;

    /// <summary>
    /// Toggles the inclusion if <see cref="Loading"/> is false.
    /// </summary>
    public void ToggleInclusion()
    {
        if (Loading)
        {
            return;
        }
        DataSong.Included = !DataSong.Included;
        MusicPlayer.SetInclusion(DataSong, DataSong.Included);
    }
}
