using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickerDecorationSlot : MonoBehaviour, IDropHandler
{
    // This method will be called when the player drops the sticker.
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null && transform.childCount == 0)
        {
            Debug.Log(eventData.pointerDrag.gameObject.name);

            // Instantiate sticker
            GameObject duplicate = Instantiate(eventData.pointerDrag.gameObject, transform);

            // Locks sticker into slot.
            duplicate.transform.position = GetComponent<RectTransform>().gameObject.transform.position;
        }
    }
}
