
using System;
using Utf8Json;

namespace LegacyWrapper.Common.Serialization.Utf8Json;

public class Utf8ToJsonSerializer : IWrapperSerializer
{
    public string Serialize<T>(T obj)
    {
        //var bytes = JsonSerializer.Serialize(obj);
        var json =  JsonSerializer.ToJsonString(obj);
        return json;
    }

    public T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }

    public object? Deserialize(string json, Type type)
    {
        throw new NotImplementedException();
    }
}