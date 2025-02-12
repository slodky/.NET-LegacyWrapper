using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using LegacyWrapper.Common.Models;

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
[JsonSerializable(typeof(IEnumerable<string>))]
[JsonSerializable(typeof(string[]))]
public partial class SourceGenerationContext : JsonSerializerContext
{
}