using UnityEngine;

/// <summary>
/// Hold all replies and has the option to sen a
/// </summary>
public class Reply : MonoBehaviour
{
    public NavigationController NavigationController;
    public UIReply UIReply;
    public UIGift UIGift;
    public Gift Gift;

    private DataMail message;
    private string[] replies;

    /// <summary>
    /// Loads all replies.
    /// </summary>
    /// <returns>True if loading is successful.;</returns>
    private bool LoadMessage()
    {
        try
        {
            TEMP temp = GameObject.Find("TEMP").GetComponent<TEMP>();
            message = temp.GetMessage();
            replies = temp.GetRepliesForMessage(message);
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            return false;
        }
    }

    /// <summary>
    /// Navigate to the <see cref="Gift"/> screen to send the reply.
    /// </summary>
    /// <param name="reply">Message to send.</param>
    /// <param name="reply">Message to send.</param>
    public void SendReply(string reply)
    {
        Gift.Show(reply);
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show()
    {
        if (LoadMessage())
        {
            UIReply.ShowMessage(message);
            UIReply.ShowReplies(replies);
            NavigationController.HomeToReply();
        }
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        UIGift.ResetColor();
        NavigationController.ReplyToHome();
    }

    #endregion
}
