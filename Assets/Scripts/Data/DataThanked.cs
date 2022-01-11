using Newtonsoft.Json;
using System;

/// <summary>
/// The thanked data
/// </summary>
/// 
[Serializable]
public class DataThanked
{
    public int Id { get; set; }
    [JsonProperty("opened_at")] public DateTime? OpenedAt { get; set; }
    [JsonProperty("received_at")] public DateTime ReceivedAt { get; set; }
}
