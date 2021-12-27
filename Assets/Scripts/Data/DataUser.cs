using System;
using Newtonsoft.Json;

/// <summary>
/// The API user object
/// </summary>
[Serializable]
public class DataUser
{
    public int Id { get; set; }
    [JsonProperty("device_id")] public string DeviceId { get; set; }
}
