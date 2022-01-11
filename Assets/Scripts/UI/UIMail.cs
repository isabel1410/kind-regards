using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the mail screen.
/// </summary>
public class UIMail : MonoBehaviour
{
    [SerializeField]
    private Text uiMessage;
    [SerializeField]
    private Text uiReply;
    [SerializeField]
    private Button uiThank;
    [SerializeField]
    private GameObject giftBoxGameObject;
    [SerializeField]
    private GameObject giftRibbonGameObject;
    [SerializeField]
    private float darkenValue;

    /// <summary>
    /// Change the received gift's color
    /// </summary>
    /// <param name="color">The color you want the gift to be</param>
    public void ChangeGiftColor(Color color)
    {
        giftBoxGameObject.GetComponent<Renderer>().material.color = new Color(color.r - darkenValue, color.g - darkenValue, color.b - darkenValue, color.a);
        giftRibbonGameObject.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Shows <paramref name="mail"/> and disables the "Thank the sender" button if necessary.
    /// </summary>
    /// <param name="mail"><see cref="DataMessage"/> to show.</param>
    public void ShowReply(DataMessage mail)
    {
        uiMessage.text = mail.Request.DataText.Text;
        uiReply.text = mail.DataText.Text;
        uiThank.interactable = !mail.HasThanked;
        uiThank.GetComponentInChildren<Text>().text = mail.HasThanked ? "You thanked the sender" : "Thank the sender";
        if (mail.HasGift)
        {
            ChangeGiftColor(mail.Gift.DataCustomization.Color);
        }
    }

    public void ShowThankMessage(DataMessage mail)
    {
        uiMessage.text = mail.Request.DataText.Text;
        uiReply.text = mail.DataText.Text;
        DisableThank();
        uiThank.GetComponentInChildren<Text>().text = "The player thanks you!";
        if (mail.HasGift)
        {
            ChangeGiftColor(mail.Gift.DataCustomization.Color);
        }
    }

    /// <summary>
    /// Disables the button to thank the sender;
    /// </summary>
    public void DisableThank()
    {
        uiThank.interactable = false;
        uiThank.GetComponentInChildren<Text>().text = "You thanked the sender";
    }
}
