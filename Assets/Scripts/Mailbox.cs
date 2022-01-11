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
        try
        {
            List<DataMessage> mail = new List<DataMessage>();
            dataReplies = mail;
            if (APIManager.Instance)
            {
                mail = APIManager.Instance.DataMessages;
                mail.Sort();
                dataReplies = mail;

                APIManager.Instance.OnMessagesRefreshed.AddListener(() =>
                {
                    mail = APIManager.Instance.DataMessages;
                    mail.Sort();
                    dataReplies = mail;

                    // Update the list on the UI with the refreshed data.
                    uiMailbox.ShowReplies(dataReplies.ToArray());
                });
                APIManager.Instance.RefreshMessages();
            }
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            uiError.Show("Make sure you have an internet connection");
            return false;
        }
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
