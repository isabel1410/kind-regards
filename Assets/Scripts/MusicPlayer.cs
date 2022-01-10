using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains all logic for the music player.
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private UIMusicPlayer uiMusicPlayer;
    private DataSong[] songs;
    [SerializeField]
    private int currentSongIndex;
    [SerializeField]
    private Settings settings;
    [SerializeField]
    private bool shuffling;
    [SerializeField]
    private float volume;
    private AudioSource audioSource;

    /// <summary>
    /// Copies the settings to this script and creates the UI.
    /// </summary>
    /// <param name="dataSettings">Settings to copy.</param>
    public void ApplySettings(DataSettings dataSettings)
    {
        audioSource.volume = volume = dataSettings.MusicVolume;
        shuffling = dataSettings.MusicShuffle;
        for (sbyte index = 0; index < songs.Length; index++)
        {
            songs[index].Included = dataSettings.MusicEnabled[index];
        }

        uiMusicPlayer.FillUI(songs);
    }

    /// <summary>
    /// Applies default settings to <paramref name="dataSettings"/>.
    /// </summary>
    /// <param name="dataSettings">Settings to apply the default settings to.</param>
    public void ApplyDefaultSettings(ref DataSettings dataSettings)
    {
        dataSettings.MusicVolume = .1f;
        dataSettings.MusicShuffle = false;
        dataSettings.MusicEnabled = new bool[songs.Length];
        for (sbyte songIndex = 0; songIndex < songs.Length; songIndex++)
        {
            dataSettings.MusicEnabled[songIndex] = true;
        }
    }

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        songs = GetComponentsInChildren<DataSong>();
        audioSource = GetComponent<AudioSource>();
        if (settings.Load())
        {
            StartCoroutine(Play());
        }
    }

    /// <summary>
    /// Toggles the shuffle function.
    /// </summary>
    public void ToggleShuffle()
    {
        bool shuffling = !this.shuffling;
        settings.SetShuffle(shuffling);
        this.shuffling = shuffling;
        uiMusicPlayer.ToggleShuffle(shuffling);
    }

    /// <summary>
    /// Plays the next song in the list (random if <see cref="shuffling"/> is true).
    /// </summary>
    public void NextSong()
    {
        AudioClip audioClip = GetNextSong();
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Plays the previous song in the list (random if <see cref="shuffling"/> is true).
    /// </summary>
    public void PreviousSong()
    {
        AudioClip audioClip = GetPreviousSong();
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    /// <summary>
    /// Gets the previous song in the list (random if <see cref="shuffling"/> is true).
    /// </summary>
    /// <returns>Audio file of the previous song.</returns>
    private AudioClip GetPreviousSong()
    {
        List<AudioClip> songs = new List<AudioClip>();
        foreach (DataSong dataSong in this.songs)
        {
            if (dataSong.Included)
            {
                songs.Add(dataSong.AudioClip);
            }
        }
        if (songs.Count == 0)
        {
            return null;
        }
        if (shuffling && songs.Count != 1)
        {
            //Get random index (but not the same)
            int currentIndex = currentSongIndex;
            do
            {
                currentSongIndex = Random.Range(0, songs.Count);
            }
            while (currentSongIndex == currentIndex);
        }
        else
        {
            //Get next index
            currentSongIndex--;
            if (currentSongIndex == -1)
            {
                currentSongIndex = songs.Count - 1;
            }
        }
        return songs[currentSongIndex];
    }

    /// <summary>
    /// Gets the next song in the list (random if <see cref="shuffling"/> is true).
    /// </summary>
    /// <returns>Audio file of the next song.</returns>
    private AudioClip GetNextSong()
    {
        List<AudioClip> songs = new List<AudioClip>();
        foreach (DataSong dataSong in this.songs)
        {
            if (dataSong.Included)
            {
                songs.Add(dataSong.AudioClip);
            }
        }
        if (songs.Count == 0)
        {
            return null;
        }
        if (shuffling && songs.Count != 1)
        {
            //Get random index (but not the same)
            int currentIndex = currentSongIndex;
            do
            {
                currentSongIndex = Random.Range(0, songs.Count);
            }
            while (currentSongIndex == currentIndex);
        }
        else
        {
            //Get next index
            currentSongIndex++;
            if (currentSongIndex == songs.Count)
            {
                currentSongIndex = 0;
            }
        }
        return songs[currentSongIndex];
    }

    /// <summary>
    /// Infinitely plays songs. If there are no songs in the list, it will refresh every 3 seconds to include updates.
    /// </summary>
    /// <returns></returns>
    private System.Collections.IEnumerator Play()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        while (true)
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            NextSong();
            yield return new WaitForSeconds(3);
        }
    }

    /// <summary>
    /// Toggle a song to be included or excluded in the list.
    /// </summary>
    /// <param name="song">Song to include or exclude.</param>
    /// <param name="included">Whether or not this song should be included.</param>
    public void SetInclusion(DataSong song, bool included)
    {
        settings.SetInclusion(songs.ToList().IndexOf(song), included);
    }

    /// <summary>
    /// Sets the volume of the audio source.
    /// </summary>
    /// <param name="volume"></param>
    public void SetVolume(float volume)
    {
        settings.SetVolume(volume);
        audioSource.volume = volume;
    }

    /// <summary>
    /// Pauses or resumes playing of music.
    /// </summary>
    public void TogglePause()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        bool paused = audioSource.pitch == 1;//Already toggled
        audioSource.pitch = paused ? 0 : 1;
        uiMusicPlayer.TogglePause(paused);
    }

    /// <summary>
    /// Updates the UI of the music player.
    /// </summary>
    public void Show()
    {
        uiMusicPlayer.ToggleShuffle(shuffling);
        uiMusicPlayer.SetVolume(volume);
    }
}
