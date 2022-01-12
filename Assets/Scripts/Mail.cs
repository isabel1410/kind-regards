using UnityEngine;

/// <summary>
/// Handles Mail actions.
/// </summary>
public class Mail : MonoBehaviour
{
    [SerializeField]
    private UIMail uiMail;
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private DataMessage dataMail;
    [SerializeField]
    private StickerBook stickerBook;

    /// <summary>
    /// Thanks the sender of the reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    public void ThankSender()
    {
        //print(dataMail.DataText.Text + ": Thanked sender");
        // If the API exists send out a request to thank the sender.
        if (APIManager.Instance) APIManager.Instance.SendMessageThanks(dataMail);

        // Disable the thank button when the thanks is sent.
        uiMail.DisableThank();
    }

    /// <summary>
    /// Opens the gift of a reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Animation not included</exception>
    public void OpenGift()
    {
        //print(dataMail.DataText.Text + ": Opened gift");

        // If the sticker is new it will be unlocked and get back if it was new.
        bool isNew = stickerBook.UnlockSticker(dataMail.Gift.DataSticker);

        // Exit out of the message.
        Exit();

        // If the sticker was new show it in the stickerbook.
        if (isNew)
        {
            stickerBook.Show();
        }
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    /// <param name="mail">The mail to be shown in the UI</param>
    public void Show(DataMessage mail)
    {
        // Get the user of the API.
        DataUser dataUser = null;
        if (APIManager.Instance) dataUser = APIManager.Instance.DataUser;
        else return;

        dataMail = mail;
        
        // If the mail is not seen yet it will be marked as seen.
        if (!dataMail.Seen) dataMail.MarkSeen();

        if (dataMail.Request.RequesterId == dataUser.Id)
        {
            // If the requester is the same as the current user it should show the message as received. (They are the receiver)
            uiMail.ShowReply(dataMail);
        }
        else
        {
            // If the requester is not the same as the current user it should show that he is thanked. (They are the sender)
            uiMail.ShowThankMessage(dataMail);
        }
        // Dont include a gift if you are being thanked.
        navigationController.MailboxToMail(dataMail.HasGift && dataMail.Request.RequesterId == dataUser.Id);
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        if (APIManager.Instance)
        {
            // If the API exist check if the mail has a message and if the requester is the current user.
            navigationController.MailToMailbox(dataMail.HasGift && dataMail.Request.RequesterId == APIManager.Instance.DataUser.Id);
        }
        else
        {
            // If the API is not set (shouldn't happen) it should only check if the mail has a gift.
            navigationController.MailToMailbox(dataMail.HasGift);
        }
    }

    #endregion
}
