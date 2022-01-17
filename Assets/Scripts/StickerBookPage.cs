using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class StickerBookPage : MonoBehaviour
{
    [SerializeField]
    private Transform[] stickerSpawns;
    private DataSticker[] stickersOnPage;
    public int defaultChildCount = 0;

    public UnityEvent<DataSticker> OnStickerSelect;

    private void Awake()
    {
        stickersOnPage = new DataSticker[stickerSpawns.Length];
        if(stickerSpawns.Length > 0) defaultChildCount = stickerSpawns[0].childCount;
    }

    /// <summary>
    /// Create the sticker object in the world
    /// </summary>
    /// <param name="sticker">The gameobject of the sticker</param>
    public GameObject SetSticker(DataSticker sticker)
    {
        // Get the spawn location for the sticker.
        for(int i =0; i < stickerSpawns.Length; i++)
        {
            Transform spawn = stickerSpawns[i];
            // If there is a spawn instantiate the sticker on that spawn.
            if (spawn && spawn.childCount == defaultChildCount)
            {
                stickersOnPage[i] = sticker;
                GameObject obj = Instantiate(sticker.GetStickerObject(), spawn);
                obj.AddComponent<DataHolder>().Data = sticker;
                return obj;
            }
        }
        return null;
    }

    public void StickerSelected(int positionId)
    {
        OnStickerSelect?.Invoke(stickersOnPage[positionId]);
    }
}
