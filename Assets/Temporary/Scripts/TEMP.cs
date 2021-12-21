using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which will be removed when releasing.
/// </summary>
public class TEMP : MonoBehaviour
{
    public DataReply DataReply;
    public DataGift DataGift;
    public DataMail DataMail;

    /// <summary>
    /// Called before the first frame update.
    /// </summary>
    private void Start()
    {
        GetComponent<UnityEngine.UI.RawImage>().color = Color.green;
    }

    /// <summary>
    /// Get messages which can be sent out.
    /// </summary>
    /// <example>"I feel lonely", "I feel ignored".</example>
    /// <returns>Messages.</returns>
    public string[] GetRequestMessages()
    {
        List<string> requestMessages = new List<string>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            requestMessages.Add("Request message " + counter);
        }
        return requestMessages.ToArray();
    }

    /// <summary>
    /// Get replies sent as response to a request.
    /// </summary>
    /// <example>"I see you", "You are not alone".</example>
    /// <returns>Replies.</returns>
    public DataReply[] GetReplies()
    {
        List<DataReply> mail = new List<DataReply>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMinutes(-UnityEngine.Random.Range(0, 1000000));
            DataReply newMail = Instantiate(DataReply);
            newMail.gameObject.hideFlags = HideFlags.HideInHierarchy;
            newMail.Gift = UnityEngine.Random.value < .5 ? DataGift : null;
            newMail.SentMessage = "Message " + counter;
            newMail.DateTime = dateTime;
            newMail.Id = counter;
            newMail.Thanked = UnityEngine.Random.value < .5;
            newMail.Seen = UnityEngine.Random.value < .5;
            mail.Add(newMail);
        }
        return mail.ToArray();
    }

    /// <summary>
    /// Get a message the player could have sent.
    /// </summary>
    /// <returns>Message</returns>
    public DataMail GetMessage()
    {
        return Instantiate(DataMail);
    }

    /// <summary>
    /// Get replies possible for the sent message.
    /// </summary>
    /// <param name="_">Message to get reply messages from.</param>
    /// <returns>Reply messages.</returns>
    public string[] GetRepliesForMessage(DataMail _)
    {
        List<string> replies = new List<string>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            replies.Add("Reply " + counter);
        }
        return replies.ToArray();
    }
}
