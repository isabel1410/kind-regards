using System;
using UnityEngine;

public class TEMP : MonoBehaviour
{
    public DataReply DataReply;
    public DataGift Gift;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponent<UnityEngine.UI.RawImage>().color = Color.green;
    }

    public string[] GetRequestMessages()
    {
        System.Collections.Generic.List<string> requestMessages = new System.Collections.Generic.List<string>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            requestMessages.Add("Request message " + counter);
        }
        return requestMessages.ToArray();
    }

    public DataReply[] GetReplies()
    {
        System.Collections.Generic.List<DataReply> mail = new System.Collections.Generic.List<DataReply>();
        for (byte counter = 1; counter <= 50; counter++)
        {
            DateTime dateTime = DateTime.Now;
            dateTime = dateTime.AddMinutes(-UnityEngine.Random.Range(0, 1000000));
            DataReply newMail = Instantiate(DataReply);
            newMail.gameObject.hideFlags = HideFlags.HideInHierarchy;
            newMail.Gift = UnityEngine.Random.value < .5 ? Gift : null;
            newMail.SentMessage = "Message " + counter;
            newMail.DateTime = dateTime;
            newMail.Id = counter;
            newMail.Thanked = UnityEngine.Random.value < .5;
            newMail.Seen = UnityEngine.Random.value < .5;
            mail.Add(newMail);
        }
        return mail.ToArray();
    }
}
