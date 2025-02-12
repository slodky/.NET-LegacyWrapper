using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyWrapper.Common.Serialization.SystemText;

public class CustomJsonConverterForBytes : JsonConverter<Byte[]>
{
    public override byte[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var valueString = reader.GetString();
        return Convert.FromBase64String(valueString);
    }

    public override void Write(Utf8JsonWriter writer, Byte[] value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Convert.ToBase64String(value));
    }
}