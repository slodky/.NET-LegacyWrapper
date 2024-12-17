using System;

namespace LegacyWrapper.Common.Serialization;

public interface IWrapperSerializer
{
    string Serialize<T>(T obj);
    T Deserialize<T>(string json);
    object? Deserialize(string json, Type type);
}