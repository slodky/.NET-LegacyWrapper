using System;
using System.Linq;
using LegacyWrapper.Common.Serialization;
using LegacyWrapper.Common.Serialization.SystemText;
using LegacyWrapper.Common.Serialization.Utf8Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace LegacyWrapper.Executor.Core.Tests.Serialization;

[TestClass]
public class CallDataJsonConverterTests
{
    private readonly IWrapperSerializer _serializer = new SystemJsonSerializer();
    //private readonly IWrapperSerializer _serializer = new Utf8ToJsonSerializer();
    
    [TestMethod]
    public void TestUint32Serialization()
    {
        var testObject = new CallData()
        {
            Parameters = [((uint) 32)],
            ParameterTypes = [typeof(uint)]
        };
        
        var serialized = _serializer.Serialize(testObject);
        var deserialized = _serializer.Deserialize<CallData>(serialized);

        var expectedType = testObject.Parameters.First().GetType();
        //var deserializedType = deserialized.Parameters.First().GetType();
        var deserializedValue = deserialized.Parameters.First();
        var deserializedType = deserializedValue.GetType();

        Assert.AreEqual(expectedType, deserializedType);
        Assert.AreEqual(expectedType, deserializedType);
        Assert.AreEqual(testObject.Parameters.First(), deserializedValue);
    }
    
    [TestMethod]
    public void TestWrappedUint32Serialization()
    {
        var testObject = new CallData()
        {
            Parameters = [( new Uint32Wrapper() { Value = 32})],
            ParameterTypes = [typeof(Uint32Wrapper)]
        };

        var serialized = _serializer.Serialize(testObject);
        var deserialized = _serializer.Deserialize<CallData>(serialized);

        var expectedType = testObject.Parameters.First().GetType();
        //var deserializedType = deserialized.Parameters.First().GetType();
        var deserializedValue = deserialized.Parameters.First();
        var deserializedType = deserializedValue.GetType();

        Assert.AreEqual(expectedType, deserializedType);
        Assert.AreEqual(expectedType, deserializedType);
        Assert.AreEqual(testObject.Parameters.First(), deserializedValue);
    }

    [TestMethod]
    public void TestIntPtrSerialization()
    {
        var testObject = new CallData()
        {
            Parameters = [new IntPtr(100)],
            ParameterTypes = [typeof(IntPtr)]
        };

        var serialized = _serializer.Serialize(testObject);
        var deserialized = _serializer.Deserialize<CallData>(serialized);

        var expectedType = testObject.Parameters.First().GetType();
        //var deserializedType = deserialized.Parameters.First().GetType();
        //var deserializedValue = deserialized.Parameters.First();

        var deserializedValue = deserialized.Parameters.First();
        var deserializedType = deserializedValue.GetType();

        Assert.AreEqual(expectedType, deserializedType);
        Assert.AreEqual(testObject.Parameters.First(), deserializedValue);
    }

    [TestMethod]
    public void TestByteArraySerialization()
    {
        byte[] originalBytes = "ABCDE"u8.ToArray();
        var testObject = new CallData()
        {
            Parameters = [originalBytes],
            ParameterTypes = [originalBytes.GetType()]
        };

        var serialized = _serializer.Serialize(testObject);
        var deserialized = _serializer.Deserialize<CallData>(serialized);

        var expectedType = testObject.Parameters.First().GetType();
        //var deserializedType = deserialized.Parameters.First().GetType();
        //var deserializedValue = deserialized.GetProperParam(0);
        var deserializedValue = deserialized.Parameters.First();
        var deserializedType = deserializedValue.GetType();


        Assert.AreEqual(expectedType, deserializedType);
        Assert.IsTrue(originalBytes.SequenceEqual((byte[])deserializedValue));
    }

    public class Uint32Wrapper
    {
        public uint Value { get; set; }
    }
}