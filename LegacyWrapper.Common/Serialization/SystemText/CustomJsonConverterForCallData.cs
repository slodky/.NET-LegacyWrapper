using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyWrapper.Common.Serialization;

/*public class CustomJsonConverterForCallData : JsonConverter<CallData>
{
    public override CallData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        // Caution: Deserialization of type instances like this is not recommended and should be avoided
        // since it can lead to potential security issues.

        var assemblyQualifiedName = reader.GetString();
        return Type.GetType(assemblyQualifiedName);
    }

    public override void Write(Utf8JsonWriter writer, CallData value, JsonSerializerOptions options)
    {
        var jsonObject = new JObject
        {
            { "LibraryName", value.LibraryName },
            { "ProcedureName", value.ProcedureName },
            { "Parameters", JArray.FromObject(value.Parameters) },
            { "ParameterTypes", JArray.FromObject(value.ParameterTypes) },
            { "ReturnType", value.ReturnType?.FullName },
            { "CallingConvention", value.CallingConvention.ToString() },
            { "CharSet", value.CharSet.ToString() },
            { "Status", value.Status.ToString() }
        };
        jsonObject.WriteTo(writer);
    }
}*/