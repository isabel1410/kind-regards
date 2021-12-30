using UnityEngine;

/// <summary>
/// Handles Mail actions.
/// </summary>
public class Mail : MonoBehaviour
{
    [SerializeField]
    private UIMail uiMail;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private DataMessage dataReply;
    [SerializeField]
    private DataUser dataUser;
    [SerializeField]
    private StickerBook stickerBook;

    /// <summary>
    /// Thanks the sender of the reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    public void ThankSender()
    {
        try
        {
            print(dataReply.DataText.Text + ": Thanked sender");
            //api call
            if (APIManager.Instance) APIManager.Instance.SendMessageThanks(dataReply);

            uiMail.DisableThank();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Make sure you have an internet connection");
        }
    }

    /// <summary>
    /// Opens the gift of a reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Animation not included</exception>
    public void OpenGift()
    {
        try
        {
            print(dataReply.DataText.Text + ": Opened gift");
            bool isNew = stickerBook.UnlockSticker(dataReply.Gift.DataSticker);
            Exit();
            if (isNew)
            {
                stickerBook.Show();
            }
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Make sure you have an internet connection");
        }
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show(DataMessage reply)
    {
        dataReply = reply;
        dataReply.MarkSeen();
        if (reply.Request.RequesterId == dataUser.Id)
        {
            uiMail.ShowReply(reply);
        }
        else
        {
            uiMail.ShowHeart(reply);
        }
        navigationController.MailboxToMail(reply.HasGift);
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        navigationController.MailToMailbox(dataReply.HasGift);
    }

    #endregion
}
