using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StickerDecorationSlot : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public DataSticker sticker = null;
    public UnityEvent<DataSticker> OnStickerChanged;

    // This method will be called when the player drops the sticker.
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null && transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            Debug.Log(eventData.pointerDrag.gameObject.name);
            // Add sticker data to decoration slot.
            sticker = eventData.pointerDrag.gameObject.GetComponent<DataHolder>().Data as DataSticker;

            // Set sticker
            SetSticker(sticker);

            // Call event
            OnStickerChanged?.Invoke(sticker);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        DestroyImmediate(transform.GetChild(0).gameObject);

        // Set sticker to null.
        sticker = null;

        // Set sticker
        SetSticker(sticker);

        // Call event
        OnStickerChanged?.Invoke(sticker);
    }

    public void SetSticker(DataSticker sticker)
    {
        if(transform.childCount > 0) DestroyImmediate(transform.GetChild(0).gameObject);
        if (sticker == null) return;

        // Instantiate sticker
        GameObject duplicate = Instantiate(sticker.GetStickerObject(), transform);

        // Locks sticker into slot.
        duplicate.transform.position = GetComponent<RectTransform>().gameObject.transform.position;
    }
}
