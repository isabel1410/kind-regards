using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Contains all the mails.
/// </summary>
public class Mailbox : MonoBehaviour
{
    public NavigationController NavigationController;
    public UIMailbox UIMailbox;
    public Mail Mail;

    private List<DataReply> dataReplies;

    /// <summary>
    /// Load all the replies.
    /// </summary>
    /// <returns>True if the loading succeeded.</returns>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    private bool LoadReplies()
    {
        try
        {
            //throw new System.NotImplementedException();
            List<DataReply> mail = GameObject.Find("TEMP").GetComponent<TEMP>().GetReplies().ToList();
            mail.Sort();
            dataReplies = mail;
            return true;
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
            return false;
        }
    }

    /// <summary>
    /// Transitions to the mail screen.
    /// </summary>
    /// <param name="reply">The <see cref="DataReply"/> to show.</param>
    public void OpenMail(DataReply reply)
    {
        Mail.Show(reply);
    }

    /// <summary>
    /// Removes the mail from the list.
    /// </summary>
    /// <param name="reply">The <see cref="DataReply"/> to delete.</param>
    /// <param name="sender">The <see cref="GameObject"/> to destroy.</param>
    /// <returns>True if the deletion was succesful.</returns>
    /// <exception cref="System.NotImplementedException">Api call not implemented.</exception>
    public bool DeleteMail(DataReply reply, GameObject sender)
    {
        try
        {
            //api call
            //throw new System.NotImplementedException();
        }
        catch (System.Exception exception)
        {
            Debug.LogException(exception);
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
