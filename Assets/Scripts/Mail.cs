using UnityEngine;

/// <summary>
/// Handles Mail actions.
/// </summary>
public class Mail : MonoBehaviour
{
    [SerializeField]
    private UIMail UIMail;
    [SerializeField]
    private UIError UIError;
    [SerializeField]
    private NavigationController NavigationController;
    [SerializeField]
    private DataMessage DataReply;
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
            print(DataReply.DataText.Text + ": Thanked sender");
            //api call
            if (APIManager.Instance) APIManager.Instance.SendMessageThanks(DataReply);

            UIMail.DisableThank();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
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
            print(DataReply.DataText.Text + ": Opened gift");
            if (!DataReply.Seen) DataReply.MarkSeen();
            bool isNew = stickerBook.UnlockSticker(DataReply.Gift.DataSticker);
            Exit();
            if (isNew)
            {
                stickerBook.Show();
            }
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
        }
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show(DataMessage reply)
    {
        DataReply = reply;
        if(!DataReply.Seen && !reply.HasGift) DataReply.MarkSeen();
        UIMail.ShowMail(DataReply);
        NavigationController.MailboxToMail(reply.HasGift);
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        NavigationController.MailToMailbox(DataReply.HasGift);
    }

    #endregion
}
