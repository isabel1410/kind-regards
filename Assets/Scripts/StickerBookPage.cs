using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StickerBookPage : MonoBehaviour
{
    public Transform[] stickerSpawns;
    
    public void SetSticker(GameObject sticker)
    {
        Transform spawn = stickerSpawns.FirstOrDefault(t => t.childCount == 1);
        if (spawn)
        {
            Instantiate(sticker, spawn);
        }
    }
}
