using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the music section of the settings screen.
/// </summary>
public class UIMusicPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject songGameObjectPrefab;
    [SerializeField]
    private Transform songContainerTransform;
    [SerializeField]
    private Button uiPlayPause;
    [SerializeField]
    private Button uiShuffle;
    [SerializeField]
    private Slider uiVolume;
    [SerializeField]
    private MusicPlayer musicPlayer;
    [SerializeField]
    private Sprite imagePlay;
    [SerializeField]
    private Sprite imagePause;
    [SerializeField]
    private Sprite imageUnShuffle;
    [SerializeField]
    private Sprite imageShuffle;

    /// <summary>
    /// Fills the <see cref="songContainerTransform"/> <see cref="GameObject"/> with songs from <see cref="songGameObjectPrefab"/>.
    /// </summary>
    /// <param name="songs">Songs to add to the container.</param>
    public void FillUI(DataSong[] songs)
    {
        float positionY = 0;
        foreach (DataSong song in songs)
        {
            GameObject songGameObject = Instantiate(songGameObjectPrefab);
            RectTransform rectTransform = songGameObject.GetComponent<RectTransform>();

            songGameObject.transform.SetParent(songContainerTransform, false);
            rectTransform.anchoredPosition = new Vector2(0, positionY);

            songGameObject.GetComponent<UISongInstantiator>().Instantiate(song, musicPlayer);

            positionY -= rectTransform.rect.height;
        }

        //Set the height of the container based on the amount of songs and the height of the prefab
        RectTransform containerRectTransform = songContainerTransform.GetComponent<RectTransform>();
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
        uiPlayPause.GetComponent<Image>().sprite = paused ? imagePlay : imagePause;
    }

    /// <summary>
    /// Switches the image of the shuffle button.
    /// </summary>
    /// <param name="shuffled">True if the music is shuffled.</param>
    public void ToggleShuffle(bool shuffled)
    {
        uiShuffle.GetComponent<Image>().sprite = shuffled ? imageUnShuffle : imageShuffle;
    }

    /// <summary>
    /// Sets the slider for the volume.
    /// </summary>
    /// <param name="volume">Volume of the music.</param>
    /// <remarks>Only used when loading.</remarks>
    public void SetVolume(float volume)
    {
        uiVolume.value = volume;
    }
}
