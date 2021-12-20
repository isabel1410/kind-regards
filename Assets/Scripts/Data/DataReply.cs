using System;
/// <summary>
/// Reply to a sent request with an optional gift.
/// </summary>
/// 
[Serializable]
public class DataReply : DataMail
{
    public DataGift Gift;
    public string ReplyMessage;
    public bool Thanked;
    public bool Seen;
}
