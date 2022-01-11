using System.Collections;
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
    private Gift gift;

    private DataRequest message;
    private DataText[] replies;

    /// <summary>
    /// Loads all replies.
    /// </summary>
    /// <returns>True if loading is successful.;</returns>
    private  bool LoadMessage()
    {
        if (APIManager.Instance)
        {
            if (APIManager.Instance.DataRequests.Count > 0) message = APIManager.Instance.DataRequests.FindAll(r => r.RequesterId != APIManager.Instance.DataUser.Id).Random();
            else return false;
            replies = APIManager.Instance.DataTexts.FindAll(t => t.Category.Name == "RESPONSE").ToArray();
            return true;
        }
        //TEMP temp = GameObject.Find("TEMP").GetComponent<TEMP>();
        //message = temp.GetMessage();
        //replies = temp.GetRepliesForMessage(message);
        return false;
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
        StartCoroutine(ShowCoroutine());
    }

    /// <summary>
    /// Refreshes the requests in the API. When an error occurs the error should show.
    /// </summary>
    /// <returns>A coroutine</returns>
    public IEnumerator ShowCoroutine()
    {
        bool hasRefreshedRequests = false;
        bool hasError = false;

        if (!APIManager.Instance)
        {
            yield break;
        }

        APIManager.Instance.OnRequestsRefreshed.AddListener(() =>
        {
            hasRefreshedRequests = true;
        });
        APIManager.Instance.OnAPIError.AddListener(ex =>
        {
            hasError = true;
        });
        APIManager.Instance.RefreshRequests();
        yield return new WaitUntil(() => hasRefreshedRequests || hasError);

        if (hasError) yield break;

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
