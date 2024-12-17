using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyWrapper.Common.Serialization;

public class CustomJsonConverterForIntPtr : JsonConverter<IntPtr>
{
    public override IntPtr Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        IntPtr result;
        bool success = false;
        try
        {
            var intPtrString = reader.GetString();
            success = IntPtrExtensions.TryParse(intPtrString, out result);

        }
        catch (InvalidOperationException)
        {
            success = reader.TryGetInt64(out var intPtrLong);
            result = (IntPtr)intPtrLong;
        }

        if (success)
        {
            return result;
        }

        throw new ArgumentException();
    }

    public override void Write(Utf8JsonWriter writer, IntPtr value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}