using System.Collections;
using System.Collections.Generic;
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
        // If API is not set it shouldn't do anything.
        if (APIManager.Instance)
        {
            // Get all requests that are made by anyone except the current user.
            List<DataRequest> requests = APIManager.Instance.DataRequests.FindAll(r => r.RequesterId != APIManager.Instance.DataUser.Id);

            // If there is more then 0 requests get a random request.
            if (requests.Count > 0) message = requests.Random();
            else return false;

            // Load all possible replies.
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
        // Start the show coroutine.
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

        // If the API is not set break out of the coroutine.
        if (!APIManager.Instance)
        {
            yield break;
        }

        // Start listening to the API events.
        APIManager.Instance.OnRequestsRefreshed.AddListener(() =>
        {
            hasRefreshedRequests = true;
        });
        APIManager.Instance.OnAPIError.AddListener(ex =>
        {
            hasError = true;
        });

        // Refresh the requests in the database.
        APIManager.Instance.RefreshRequests();

        // Wait until either the API had an error or if the requests are refreshed.
        yield return new WaitUntil(() => hasRefreshedRequests || hasError);

        // If the API had an error exit out of the coroutine.
        if (hasError) yield break;

        // Do the normal loading of a random message and show the UI.
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
