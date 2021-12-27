using Newtonsoft.Json;
using System;

[Serializable]
public class DataText
{
    public int Id { get; set; }
    [JsonProperty("category_id")] public int CategoryId { get; set; }
    public DataTextCategory Category 
    { 
        get
        {
            return APIManager.Instance ? APIManager.Instance.DataTextCategories.Find(c => c.Id == CategoryId) : null;
        }
    }
    public string Text { get; set; }
}