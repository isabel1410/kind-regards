using Newtonsoft.Json;
using System;
using UnityEngine;

/// <summary>
/// Used to store the customization of the companion.
/// </summary>
[Serializable]
public class DataCustomization
{
    public Color Color { get; set; }
}

public class ColorConverter : JsonConverter<Color>
{
    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        dynamic color = reader.Value;
        return new Color(color.r, color.g, color.b, color.a);
    }

    public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
    {
        writer.WriteStartObject();
        writer.WritePropertyName("r");
        writer.WriteValue(value.r);
        writer.WritePropertyName("g");
        writer.WriteValue(value.g);
        writer.WritePropertyName("b");
        writer.WriteValue(value.b);
        writer.WritePropertyName("a");
        writer.WriteValue(value.a);
        writer.WriteEndObject();
    }
}