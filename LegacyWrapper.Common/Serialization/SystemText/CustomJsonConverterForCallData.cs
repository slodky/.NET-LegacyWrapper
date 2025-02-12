using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using LegacyWrapper.Common.Models;

namespace LegacyWrapper.Common.Serialization.SystemText;

public class CustomJsonConverterForCallData : JsonConverter<CallData>
{
    public override CallData Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var callData = new CallData();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                return callData;

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                string propertyName = reader.GetString();
                reader.Read();

                switch (propertyName)
                {
                    case "LibraryName":
                        callData.LibraryName = reader.GetString();
                        break;

                    case "ProcedureName":
                        callData.ProcedureName = reader.GetString();
                        break;
                    
                    case "ParameterTypes":
                        callData.ParameterTypes = JsonSerializer.Deserialize<Type[]>(ref reader, options);
                        break;

                    case "Parameters":
                        var objectParams = JsonSerializer.Deserialize<object[]>(ref reader, options);
                        callData.Parameters = new object[objectParams.Length];
                        for (int index = 0; index < objectParams.Length; index++)
                        {
                            var paramObject = objectParams[index];
                            var paramType = callData.ParameterTypes?[index];
                            if (paramType == typeof(byte[]))
                            {
                                callData.Parameters[index] = Convert.FromBase64String(paramObject.ToString());
                            }
                            else
                            {
                                var paramTypeToSerialize = paramType;
                                if (paramType.IsByRef)
                                {
                                    paramTypeToSerialize = paramType.GetElementType();
                                }
                                callData.Parameters[index] = JsonSerializer.Deserialize(paramObject.ToString(), paramTypeToSerialize, options);
                            }
                        }
                        break;



                    case "ReturnType":
                        string returnTypeString = reader.GetString();
                        callData.ReturnType = Type.GetType(returnTypeString);
                        break;

                    case "CallingConvention":
                        if (Enum.TryParse(reader.GetString(), out CallingConvention callingConvention))
                            callData.CallingConvention = callingConvention;
                        break;

                    case "CharSet":
                        if (Enum.TryParse(reader.GetString(), out CharSet charSet))
                            callData.CharSet = charSet;
                        break;

                    case "Status":
                        if (Enum.TryParse(reader.GetString(), out KeepAliveStatus status))
                            callData.Status = status;
                        break;

                    default:
                        // Skip unknown properties
                        reader.Skip();
                        break;
                }
            }
        }

        throw new JsonException("Invalid JSON format for CallData");
    }

    public override void Write(Utf8JsonWriter writer, CallData value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WriteString("LibraryName", value.LibraryName);
        writer.WriteString("ProcedureName", value.ProcedureName);
        
        if (value.ParameterTypes != null)
        {
            writer.WritePropertyName("ParameterTypes");
            JsonSerializer.Serialize(writer, value.ParameterTypes, options);
        }

        if (value.Parameters != null)
        {
            writer.WritePropertyName("Parameters");
            JsonSerializer.Serialize(writer, value.Parameters, options);
        }

        if (value.ReturnType != null)
        {
            writer.WritePropertyName("ReturnType");
            writer.WriteStringValue(value.ReturnType.AssemblyQualifiedName);
        }

        writer.WriteString("CallingConvention", value.CallingConvention.ToString());
        writer.WriteString("CharSet", value.CharSet.ToString());
        writer.WriteString("Status", value.Status.ToString());

        writer.WriteEndObject();
    }
}