/*using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LegacyWrapper.Common.Serialization;

public class CallDataJsonConverter : JsonConverter<CallData>
{
    public CallDataJsonConverter()
    {
        
    }
    
    public override void WriteJson(JsonWriter writer, CallData? value, JsonSerializer serializer)
    {
        var jsonObject = new JObject
        {
            {"LibraryName", value.LibraryName},
            {"ProcedureName", value.ProcedureName},
            {"Parameters", JArray.FromObject(value.Parameters)},
            {"ParameterTypes", JArray.FromObject(value.ParameterTypes)},
            {"ReturnType", value.ReturnType?.FullName},
            {"CallingConvention", value.CallingConvention.ToString()},
            {"CharSet", value.CharSet.ToString()},
            {"Status", value.Status.ToString()}
        };
        jsonObject.WriteTo(writer);
    }

    public override CallData? ReadJson(JsonReader reader, Type objectType, CallData? existingValue,
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);
        var callData = new CallData();
        serializer.Populate(jsonObject.CreateReader(), callData);

        // Deserialize Parameters into specified types
        if (callData.Parameters.Length == callData.ParameterTypes.Length)
        {
            for (var i = 0; i < callData.Parameters.Length; i++)
            {
                callData.Parameters[i] = jsonObject["Parameters"][i].ToObject(callData.ParameterTypes[i], serializer);
            }
        }

        return callData;
    }
}*/