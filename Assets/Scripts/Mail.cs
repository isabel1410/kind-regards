using UnityEngine;

/// <summary>
/// Handles Mail actions.
/// </summary>
public class Mail : MonoBehaviour
{
    public UIMail UIMail;
    public NavigationController NavigationController;
    public DataReply DataReply;

    /// <summary>
    /// Thanks the sender of the reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    public void ThankSender()
    {
        print(DataReply.SentMessage + ": Thanked sender");
        UIMail.UIThank.interactable = false;
        //api call
        if (APIManager.Instance) APIManager.Instance.SendMessageThanks(DataReply);
    }

    /// <summary>
    /// Opens the gift of a reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Animation not included</exception>
    public void OpenGift()
    {
        print(DataReply.SentMessage + ": Opened gift");
        throw new System.NotImplementedException();
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show(DataReply reply)
    {
        DataReply = reply;
        DataReply.Seen = true;
        UIMail.ShowMail(DataReply);
        NavigationController.MailboxToMail(reply.Gift != null);
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        NavigationController.MailToMailbox(DataReply.Gift != null);
    }

    #endregion
}
