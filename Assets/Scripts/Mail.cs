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
        print(dataMail.DataText.Text + ": Thanked sender");
        //api call
        if (APIManager.Instance) APIManager.Instance.SendMessageThanks(dataMail);

        uiMail.DisableThank();
    }

    /// <summary>
    /// Opens the gift of a reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Animation not included</exception>
    public void OpenGift()
    {
        print(dataMail.DataText.Text + ": Opened gift");
        bool isNew = stickerBook.UnlockSticker(dataMail.Gift.DataSticker);
        Exit();
        if (isNew)
        {
            stickerBook.Show();
        }
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show(DataMessage mail)
    {
        DataUser dataUser = null;
        if (APIManager.Instance) dataUser = APIManager.Instance.DataUser;
        else return;

        dataMail = mail;
        if (!dataMail.Seen) dataMail.MarkSeen();
        if (dataMail.Request.RequesterId == dataUser.Id)
        {
            uiMail.ShowReply(dataMail);
        }
        else
        {
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
        navigationController.MailToMailbox(dataMail.HasGift);
    }

    #endregion
}
