using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the mail screen.
/// </summary>
public class UIMail : MonoBehaviour
{
    public Text UIMessage;
    public Text UIReply;
    public Button UIThank;

    /// <summary>
    /// Shows <paramref name="mail"/> and disables the "Thank the sender" button if necessary.
    /// </summary>
    /// <param name="mail"><see cref="DataReply"/> to show.</param>
    public void ShowMail(DataReply mail)
    {
        UIMessage.text = mail.SentMessage;
        UIReply.text = mail.ReplyMessage;
        UIThank.interactable = !mail.Thanked;
    }
}
