using UnityEngine;

/// <summary>
/// Contains data of a song used in <see cref="MusicPlayer"/>.
/// </summary>
public class DataSong : MonoBehaviour
{
    public string Name;
    public bool Included = true;
    public AudioClip AudioClip;

    /// <summary>
    /// Checks if the necessary variables are assigned and valid.
    /// </summary>
    private void OnValidate()
    {
        if (AudioClip == null)
        {
            Debug.LogError(this + ": AudioClip is not assigned", gameObject);
        }
        if (string.IsNullOrWhiteSpace(Name))
        {
            Debug.LogError(this + ": Name is not assigned", gameObject);
        }
    }
}
