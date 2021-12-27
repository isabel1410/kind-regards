using System;
using Newtonsoft.Json;

/// <summary>
/// Base mail.
/// </summary>
/// 
[Serializable]
public class DataRequest
{
    public int Id { get; set; }
    [JsonProperty("requester_id")] public int RequesterId { get; set; }
    public DataText DataText { 
        get
        {
            return APIManager.Instance ? APIManager.Instance.DataTexts.Find(t => t.Id == TextId) : null;
        }
    }
    [JsonProperty("text_id")] public int TextId { get; set; }
}
