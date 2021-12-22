using UnityEngine;

/// <summary>
/// Handles Mail actions.
/// </summary>
public class Mail : MonoBehaviour
{
    public UIMail UIMail;
    public UIError UIError;
    public NavigationController NavigationController;
    public DataMessage DataReply;

    /// <summary>
    /// Thanks the sender of the reply.
    /// </summary>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    public void ThankSender()
    {
        try
        {
            print(DataReply.DataText.Text + ": Thanked sender");
            UIMail.UIThank.interactable = false;
            //api call
            if (APIManager.Instance) APIManager.Instance.SendMessageThanks(DataReply);

            UIMail.UIThank.interactable = false;
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
        if(!DataReply.Seen && !DataReply.Gift) DataReply.MarkSeen();
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
