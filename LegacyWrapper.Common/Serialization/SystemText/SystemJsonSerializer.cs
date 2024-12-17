using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LegacyWrapper.Common.Serialization.SystemText;

public class SystemJsonSerializer : IWrapperSerializer
{
    private readonly JsonSerializerOptions _options;

    public SystemJsonSerializer()
    {
        _options = new JsonSerializerOptions()
        {
            TypeInfoResolver = SourceGenerationContext.Default,
            WriteIndented = true,
            ReferenceHandler = ReferenceHandler.Preserve
        };
        _options.Converters.Add(new CustomJsonConverterForType());
        _options.Converters.Add(new CustomJsonConverterForIntPtr());
    }
    
    public string Serialize<T>(T obj)
    {
        return JsonSerializer.Serialize(obj, _options);
    }

    public T Deserialize<T>(string json)
    {
        return JsonSerializer.Deserialize<T>(json, _options);
    }
    
    public object? Deserialize(string json, Type type)
    {
        return JsonSerializer.Deserialize(json, type, _options);
    }
}