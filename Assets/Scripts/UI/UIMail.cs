using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Contains functions to update the UI of the mail screen.
/// </summary>
public class UIMail : MonoBehaviour
{
    [SerializeField]
    private Text UIMessage;
    [SerializeField]
    private Text UIReply;
    [SerializeField]
    private Button UIThank;

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

    /// <summary>
    /// Disables the button to thank the sender;
    /// </summary>
    public void DisableThank()
    {
        UIThank.interactable = false;
    }
}
