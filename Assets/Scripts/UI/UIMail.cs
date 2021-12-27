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
    private GameObject giftGameObject;

    /// <summary>
    /// Change the received gift's color
    /// </summary>
    /// <param name="color">The color you want the gift to be</param>
    public void ChangeGiftColor(Color color)
    {
        giftGameObject.GetComponent<Renderer>().material.color = color;
    }

    /// <summary>
    /// Shows <paramref name="mail"/> and disables the "Thank the sender" button if necessary.
    /// </summary>
    /// <param name="mail"><see cref="DataMessage"/> to show.</param>
    public void ShowMail(DataMessage mail)
    {
        uiMessage.text = mail.Request.DataText.Text;
        uiReply.text = mail.DataText.Text;
        uiThank.interactable = !mail.Thanked;
        if (mail.HasGift) ChangeGiftColor(mail.Gift.DataCustomization.Color);
    }

    /// <summary>
    /// Disables the button to thank the sender;
    /// </summary>
    public void DisableThank()
    {
        uiThank.interactable = false;
    }
}
