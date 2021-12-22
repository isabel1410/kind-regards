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
    /// <param name="mail"><see cref="DataMessage"/> to show.</param>
    public void ShowMail(DataMessage mail)
    {
        UIMessage.text = mail.Request.DataText.Text;
        UIReply.text = mail.DataText.Text;
        UIThank.interactable = !mail.Thanked;
    }
}
