using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the music section of the settings screen.
/// </summary>
public class UIMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject SongGameObjectPrefab;
    [SerializeField]
    private Transform SongContainerTransform;
    [SerializeField]
    private Button UIPlayPause;
    [SerializeField]
    private Button UIShuffle;
    [SerializeField]
    private Slider UIVolume;
    [SerializeField]
    private MusicPlayer MusicPlayer;
    [SerializeField]
    private Sprite ImagePlay;
    [SerializeField]
    private Sprite ImagePause;
    [SerializeField]
    private Sprite ImageUnShuffle;
    [SerializeField]
    private Sprite ImageShuffle;

    /// <summary>
    /// Fills the <see cref="SongContainerTransform"/> <see cref="GameObject"/> with songs from <see cref="SongGameObjectPrefab"/>.
    /// </summary>
    /// <param name="songs">Songs to add to the container.</param>
    public void FillUI(DataSong[] songs)
    {
        float positionY = 0;
        foreach (DataSong song in songs)
        {
            GameObject songGameObject = Instantiate(SongGameObjectPrefab);
            RectTransform rectTransform = songGameObject.GetComponent<RectTransform>();

            songGameObject.transform.SetParent(SongContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            songGameObject.GetComponent<UISongInstantiator>().Instantiate(song, MusicPlayer);

            positionY -= rectTransform.rect.height;
        }

        //Set the height of the container based on the amount of songs and the height of the prefab
        RectTransform containerRectTransform = SongContainerTransform.GetComponent<RectTransform>();
        containerRectTransform.sizeDelta = new Vector2(
            containerRectTransform.sizeDelta.x,
            Mathf.Abs(positionY) - containerRectTransform.rect.height);
    }

    /// <summary>
    /// Switches the image of the pause button.
    /// </summary>
    /// <param name="paused">True if the music is paused.</param>
    public void TogglePause(bool paused)
    {
        UIPlayPause.GetComponent<Image>().sprite = paused ? ImagePlay : ImagePause;
    }

    /// <summary>
    /// Switches the image of the shuffle button.
    /// </summary>
    /// <param name="shuffled">True if the music is shuffled.</param>
    public void ToggleShuffle(bool shuffled)
    {
        UIShuffle.GetComponent<Image>().sprite = shuffled ? ImageUnShuffle : ImageShuffle;
    }

    /// <summary>
    /// Sets the slider for the volume.
    /// </summary>
    /// <param name="volume">Volume of the music.</param>
    /// <remarks>Only used when loading.</remarks>
    public void SetVolume(float volume)
    {
        UIVolume.value = volume;
    }
}
