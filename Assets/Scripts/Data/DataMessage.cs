using System;
using Newtonsoft.Json;

/// <summary>
/// Reply to a sent request with an optional gift.
/// </summary>
/// 
[Serializable]
public class DataMessage : IComparable<DataMessage>
{
    public int Id { get; set; }
    [JsonProperty("request_id")] public int RequestId { get; set; }
    public DataRequest Request
    {
        get
        {
            return APIManager.Instance ? APIManager.Instance.DataRequests.Find(t => t.Id == RequestId) : null;
        }
    }

    [JsonProperty("gift_id")] public int? GiftId { get; set; }
    public bool HasGift => GiftId != null;
    public DataGift Gift;
    
    [JsonProperty("text_id")] public int TextId { get; set; }
    public DataText DataText
    {
        get
        {
            return APIManager.Instance ? APIManager.Instance.DataTexts.Find(t => t.Id == TextId) : null;
        }
    }

    [JsonProperty("thanks_id")] public int? ThanksId { get; set; }
    public bool HasThanked => ThanksId != null;
    public DataThanked Thanks;

    [JsonProperty("opened_at")] public DateTime? OpenedAt { get; set; }
    public bool Seen
    {
        get
        {
            return OpenedAt != null;
        }
    }

    [JsonProperty("received_at")] public DateTime ReceivedAt { get; set; }

    /// <summary>
    /// For sorting purposes. Sorts on <see cref="DateTime"/>.
    /// </summary>
    /// <param name="other">The mail to compare this mail to.</param>
    /// <returns>-1 when <paramref name="other"/> is earlier, 0 when it is the same day, 1 when it is later</returns>
    public int CompareTo(DataMessage other)
    {
        return other.ReceivedAt.CompareTo(ReceivedAt);
    }

    public void MarkSeen()
    {
        if (this.Request.RequesterId == APIManager.Instance.DataUser.Id) APIManager.Instance.MarkMessageSeen(this);
        else APIManager.Instance.MarkThankedSeen(this);
    }
}
