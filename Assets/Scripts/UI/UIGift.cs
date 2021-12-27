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
    private GameObject giftGameObject;

    public Color GiftStartColor { get; private set; }

    /// <summary>
    /// Called before the start of the first frame.
    /// </summary>
    private void Start()
    {
        GiftStartColor = giftGameObject.GetComponent<Renderer>().material.color;
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
        giftGameObject.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Resets the color of the gift.
    /// </summary>
    public void ResetColor()
    {
        giftGameObject.GetComponent<Renderer>().material.color = GiftStartColor;
    }
}
