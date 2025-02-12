using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LegacyWrapper.Executor.Core.Tests.Serialization;

[TestClass]
public class SourceGenSerializerTests
{

    [TestMethod]

    public void Test()
    {
        var sampleCallData = new CallData()
        {
            Parameters = [1],
            ParameterTypes = [typeof(int)],
            Status = KeepAliveStatus.KeepAlive,
            CallingConvention = CallingConvention.StdCall,
            LibraryName = "test.dll",
            ProcedureName = "DoTest",
            CharSet = CharSet.Auto,
            ReturnType = typeof(string)
        };
        
        var options = new JsonSerializerOptions()
        {
            TypeInfoResolver = SourceGenerationContext.Default
        };
        options.Converters.Add(new CustomJsonConverterForType());
        
        var serialized = JsonSerializer.Serialize(sampleCallData, options);
        Console.WriteLine("Sending: {0}", serialized);
        
        CallData callDataDeserialized = JsonSerializer.Deserialize<CallData>(serialized, options);
            
        Console.WriteLine("done");

    }
}