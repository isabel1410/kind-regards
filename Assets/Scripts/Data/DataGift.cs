using Newtonsoft.Json;
using System;

/// <summary>
/// Gift containing the customization of the box and the included sticker.
/// </summary>
/// 
[Serializable]
public class DataGift
{
    //Customization
    [JsonProperty("customization")] public DataCustomization DataCustomization { get; set; }
    [JsonProperty("sticker_id")] public int StickerId { get; set; }
    public DataSticker DataSticker 
    {
        get
        {
            return APIManager.Instance ? APIManager.Instance.DataStickers.Find(s => s.Id == StickerId) : null;
        } 
    }
    //Sticker
}
