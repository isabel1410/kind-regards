using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class which will be removed when releasing.
/// </summary>
public class TEMP : MonoBehaviour
{
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
    public DataMessage[] GetReplies()
    {
        List<DataMessage> mail = new List<DataMessage>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            //DateTime dateTime = DateTime.Now;
            //dateTime = dateTime.AddMinutes(-UnityEngine.Random.Range(0, 1000000));
            //DataMessage newMail = Instantiate(DataReply);
            //newMail.gameObject.hideFlags = HideFlags.HideInHierarchy;
            //newMail.Gift = UnityEngine.Random.value < .5 ? DataGift : null;
            //newMail.SentMessage = "Message " + counter;
            //newMail.DateTime = dateTime;
            //newMail.Id = counter;
            //newMail.Thanked = UnityEngine.Random.value < .5;
            //newMail.Seen = UnityEngine.Random.value < .5;
            //mail.Add(newMail);
        }
        return mail.ToArray();
    }

    /// <summary>
    /// Get a message the player could have sent.
    /// </summary>
    /// <returns>Message</returns>
    public DataRequest GetMessage()
    {
        return null; //Instantiate(DataMail);
    }

    /// <summary>
    /// Get replies possible for the sent message.
    /// </summary>
    /// <param name="_">Message to get reply messages from.</param>
    /// <returns>Reply messages.</returns>
    public string[] GetRepliesForMessage(DataRequest _)
    {
        List<string> replies = new List<string>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            replies.Add("Reply " + counter);
        }
        return replies.ToArray();
    }

    /// <summary>
    /// Get default messages the owl can speak.
    /// </summary>
    /// <returns>Messages.</returns>
    public string[] GetCompanionMessages()
    {
        return new string[]
        {
            "You are amazing!",
            "You are doing great!",
            "How are you today?",
            "I hope you have an amazing day",
            "It's nice to see you",
            "Welcome back!"
        };
    }

    /// <summary>
    /// Get messages the owl can speak when the mood is negative.
    /// </summary>
    /// <returns>Messages.</returns>
    public string[] GetCompanionMessagesNegative()
    {
        return new string[]
        {
            "I'm here for you!",
            "I hope you're day is going well",
            "Let's do something fun today!"
        };
    }

    /// <summary>
    /// Get messages the owl can speak when the mood is positive.
    /// </summary>
    /// <returns>Messages.</returns>
    public string[] GetCompanionMessagesPositive()
    {
        return new string[]
        {
            "You rock!",
            "You're awesome!",
            "I hope today is amazing!"
        };
    }
}
