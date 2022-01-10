using UnityEngine;

/// <summary>
/// Hold all replies and has the option to sen a
/// </summary>
public class Reply : MonoBehaviour
{
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private UIReply uiReply;
    [SerializeField]
    private UIGift uiGift;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private Gift gift;

    private DataRequest message;
    private DataText[] replies;

    /// <summary>
    /// Loads all replies.
    /// </summary>
    /// <returns>True if loading is successful.;</returns>
    private bool LoadMessage()
    {
        try
        {
            if (APIManager.Instance)
            {
                APIManager.Instance.RefreshRequests();
                if (APIManager.Instance.DataRequests.Count > 0) message = APIManager.Instance.DataRequests.Random();
                else return false;
                replies = APIManager.Instance.DataTexts.FindAll(t => t.Category.Name == "RESPONSE").ToArray();
                return true;
            }
            //TEMP temp = GameObject.Find("TEMP").GetComponent<TEMP>();
            //message = temp.GetMessage();
            //replies = temp.GetRepliesForMessage(message);
            return false;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Make sure you have an internet connection");
            return false;
        }
    }

    /// <summary>
    /// Navigate to the <see cref="Gift"/> screen to send the reply.
    /// </summary>
    /// <param name="reply">Message to send.</param>
    /// <param name="reply">Message to send.</param>
    public void SendReply(DataText reply)
    {
        gift.Show(message, reply);
    }

    #region Visuals

    /// <summary>
    /// Transitions from the mailbox screen to the mail screen.
    /// </summary>
    public void Show()
    {
        if (LoadMessage())
        {
            uiReply.ShowMessage(message);
            uiReply.ShowReplies(replies);
            navigationController.HomeToReply();
        }
    }

    /// <summary>
    /// Exits to the mailbox screen.
    /// </summary>
    public void Exit()
    {
        uiGift.ResetColor();
        navigationController.ReplyToHome();
    }

    #endregion
}
