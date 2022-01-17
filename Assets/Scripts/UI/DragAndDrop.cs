using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector2 originalPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        originalPosition = GetComponent<RectTransform>().anchoredPosition;
    }

    // This method is called once at the beginning of when a sticker gets dragged.
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        Debug.Log(gameObject);
        Debug.Log(eventData.pointerDrag.gameObject);

        // Alpha to a lower amount & blocksRaycast to false so it can't block OnDrop method in StickerDecorationSlot.
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    // This method will be called every frame when a sticker gets dragged.
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");

        // Makes it so the sticker will always follow the cursor correctly. Scale of the canvas is taken into account.
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    // This method will be called once at the end of the drag action.
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");

        // Changing alpha and blocksRaycast back
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Reset to original position
        GetComponent<RectTransform>().anchoredPosition = originalPosition;
    }

    // This method will be called when a player clicks on the sticker.
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }
}
