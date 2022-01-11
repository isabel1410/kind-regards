using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the gift screen.
/// </summary>
public class UIGift : MonoBehaviour
{
    [SerializeField]
    private Text uiMessage;
    [SerializeField]
    private GameObject giftBoxGameObject;
    [SerializeField]
    private GameObject giftRibbonGameObject;
    [SerializeField]
    private float darkenValue;

    public Color GiftStartColor { get; private set; }

    /// <summary>
    /// Called before the start of the first frame.
    /// </summary>
    private void Start()
    {
        GiftStartColor = giftRibbonGameObject.GetComponent<Renderer>().material.color;
    }

    /// <summary>
    /// Displays the message.
    /// </summary>
    /// <param name="message">Message to show.</param>
    public void ShowMessage(DataText message)
    {
        uiMessage.text = message.Text;
    }

    /// <summary>
    /// Changes the color of the gift.
    /// </summary>
    /// <param name="color">The new color.</param>
    public void ChangeColor(Color color)
    {
        giftBoxGameObject.GetComponent<Renderer>().material.color = new Color(color.r - darkenValue, color.g - darkenValue, color.b - darkenValue, color.a);
        giftRibbonGameObject.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Resets the color of the gift.
    /// </summary>
    public void ResetColor()
    {
        ChangeColor(GiftStartColor);
    }
}
