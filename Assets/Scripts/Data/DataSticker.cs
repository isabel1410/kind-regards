using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class DataSticker
{
    public int Id { get; set; }
    [JsonProperty("prefab_path")] public string PrefabPath { get; set; }

    public GameObject GetStickerObject()
    {
        return Resources.Load<GameObject>(PrefabPath);
    }
}