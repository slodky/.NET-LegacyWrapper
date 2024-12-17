using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyWrapper.Common.Serialization;

public class CustomJsonConverterForType : JsonConverter<Type>
{
    public override Type Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Caution: Deserialization of type instances like this is not recommended and should be avoided
        // since it can lead to potential security issues.

        var assemblyQualifiedName = reader.GetString();
        return Type.GetType(assemblyQualifiedName);
    }

    public override void Write(Utf8JsonWriter writer, Type value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.AssemblyQualifiedName);
    }
}