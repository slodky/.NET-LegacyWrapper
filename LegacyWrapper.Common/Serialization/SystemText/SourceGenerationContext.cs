using System;
using System.Text.Json.Serialization;

namespace LegacyWrapper.Common.Serialization;

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(CallData))]
[JsonSerializable(typeof(CallResult))]
[JsonSerializable(typeof(UInt32))]
[JsonSerializable(typeof(Int16))]
[JsonSerializable(typeof(Int32))]
[JsonSerializable(typeof(Int64))]
[JsonSerializable(typeof(IntPtr))]
[JsonSerializable(typeof(Byte[]))]
[JsonSerializable(typeof(Byte))]
[JsonSerializable(typeof(string))]
[JsonSerializable(typeof(float))]
[JsonSerializable(typeof(double))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}