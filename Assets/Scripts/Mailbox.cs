using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains all the mails.
/// </summary>
public class Mailbox : MonoBehaviour
{
    [SerializeField]
    private NavigationController NavigationController;
    [SerializeField]
    private UIMailbox UIMailbox;
    [SerializeField]
    private UIError UIError;
    [SerializeField]
    private Mail Mail;

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
            if (APIManager.Instance) mail = APIManager.Instance.DataMessages;
            mail.Sort();
            dataReplies = mail;
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            UIError.Show("Make sure you have an internet connection");
            return false;
        }
    }

    /// <summary>
    /// Transitions to the mail screen.
    /// </summary>
    /// <param name="reply">The <see cref="DataMessage"/> to show.</param>
    public void OpenMail(DataMessage reply)
    {
        Mail.Show(reply);
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
            UIError.Show("Make sure you have an internet connection");
            return false;
        }
        UIMailbox.DestroyMailGameObject(sender);
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
            UIMailbox.ShowReplies(dataReplies.ToArray());
            NavigationController.HomeToMailbox();
        }
    }

    /// <summary>
    /// Exits to the home screen.
    /// </summary>
    public void Exit()
    {
        NavigationController.MailboxToHome();
    }

#endregion
}
