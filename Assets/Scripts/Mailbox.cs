using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all the mails.
/// </summary>
public class Mailbox : MonoBehaviour
{
    [SerializeField]
    private NavigationController navigationController;
    [SerializeField]
    private UIMailbox uiMailbox;
    [SerializeField]
    private UIError uiError;
    [SerializeField]
    private Mail mail;

    private List<DataMessage> dataReplies;

    /// <summary>
    /// Load all the replies.
    /// </summary>
    /// <returns>True if the loading succeeded.</returns>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    private bool LoadReplies()
    {
        // Create an empty mails list to default to.
        List<DataMessage> mail = new List<DataMessage>();

        // If the API exists it gets all the received and sent messages and then sort them by date.
        if (APIManager.Instance)
        {
            mail = APIManager.Instance.DataReceivedMessages;
            mail.AddRange(APIManager.Instance.DataSentMessages);
            mail.Sort();
        }
        
        // Set the mails to mail.
        dataReplies = mail;
        return true;
    }

    /// <summary>
    /// Transitions to the mail screen.
    /// </summary>
    /// <param name="reply">The <see cref="DataMessage"/> to show.</param>
    public void OpenMail(DataMessage reply)
    {
        mail.Show(reply);
    }

    /// <summary>
    /// Removes the mail from the list.
    /// </summary>
    /// <param name="reply">The <see cref="DataMessage"/> to delete.</param>
    /// <param name="sender">The <see cref="GameObject"/> to destroy.</param>
    /// <returns>True if the deletion was succesful.</returns>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    public bool DeleteMail(DataMessage reply, GameObject sender)
    {
        try
        {
            //api call
            throw new System.NotImplementedException();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Make sure you have an internet connection");
            return false;
        }
        uiMailbox.DestroyMailGameObject(sender);
        dataReplies.Remove(reply);
        return true;
    }

    #region Visuals

    /// <summary>
    /// Loads the replies and transitions from the home screen to the requests screen.
    /// </summary>
    public void Show()
    {
        // Start the show coroutine.
        StartCoroutine(ShowCoroutine());
    }

    /// <summary>
    /// Refreshes the messages and requests and then shows them in the messages.
    /// </summary>
    /// <returns>A coroutine</returns>
    private IEnumerator ShowCoroutine()
    {
        bool hasRefreshedMails = false;
        bool hasRefreshedRequests = false;
        bool hasError = false;
        
        // If the API doesn't exist break out of the coroutine.
        if (!APIManager.Instance) yield break;

        // Listen to the API events.
        APIManager.Instance.OnMessagesRefreshed.AddListener(() =>
        {
            hasRefreshedMails = true;
        });
        APIManager.Instance.OnRequestsRefreshed.AddListener(() =>
        {
            hasRefreshedRequests = true;
        });
        APIManager.Instance.OnAPIError.AddListener(ex =>
        {
            hasError = true;
        });

        // Refresh the messages and requests.
        APIManager.Instance.RefreshMessages();
        APIManager.Instance.RefreshRequests();

        // Wait until either the mails and requests are refreshed or the API had an error.
        yield return new WaitUntil(() => (hasRefreshedMails && hasRefreshedRequests) || hasError);

        // If the API had an error then break out of the coroutine.
        if (hasError) yield break;

        // Load the messages and show them on the UI.
        if (LoadReplies())
        {
            uiMailbox.ShowReplies(dataReplies.ToArray());
            navigationController.HomeToMailbox();
        }
    }

    /// <summary>
    /// Exits to the home screen.
    /// </summary>
    public void Exit()
    {
        navigationController.MailboxToHome();
    }

    #endregion
}
