using UnityEngine;
using System.Linq;

public class StickerBookPage : MonoBehaviour
{
    [SerializeField]
    private Transform[] stickerSpawns;

    /// <summary>
    /// Create the sticker object in the world
    /// </summary>
    /// <param name="sticker">The gameobject of the sticker</param>
    public void SetSticker(GameObject sticker)
    {
        // Get the spawn location for the sticker.
        Transform spawn = stickerSpawns.FirstOrDefault(t => t.childCount == 1);

        // If there is a spawn instantiate the sticker on that spawn.
        if (spawn)
        {
            Instantiate(sticker, spawn);
        }
    }
}
