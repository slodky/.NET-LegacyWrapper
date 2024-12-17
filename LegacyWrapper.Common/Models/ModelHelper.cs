using System;
using System.Text.Json;
using LegacyWrapper.Common.Serialization;
using LegacyWrapper.Common.Serialization.SystemText;

namespace LegacyWrapper.Common.Standard.Models;

public class ModelHelper
{
    private readonly IWrapperSerializer _serializer = new SystemJsonSerializer();
    
    public object[] EnsureSerializationTypes(object[] parameters, Type[] parameterTypes)
    {
        if (parameters != null && parameters.Length > 0)
        {
            var invokeParams = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                var param = parameters[i];
                var paramType = parameterTypes[i];
                invokeParams[i] = GetProperParam(param, paramType);
            }

            return invokeParams;
        }
        return parameters;
    }
    
    private object GetProperParam(object myParam, Type paramType)
    {
        //var myParam = _parameters[index];
        //var paramType = ParameterTypes[index];
            
        if (paramType.IsByRef)
        {
            //can't deserialize ByRef types but we don't need them to invoke function
            paramType = paramType.GetElementType();
        }

        if (myParam is JsonElement myParamJsonElement)
        {
            if (paramType == typeof(IntPtr))
            {
                var success = IntPtrExtensions.TryParse(myParamJsonElement.GetString(), out var ptr);
                if (success)
                {
                    return ptr;
                }

                throw new ArgumentException("Can't serialize IntPtr");
            }

            return _serializer.Deserialize(myParamJsonElement.GetRawText(), paramType);
        }
        //return ChangeType(myParam, paramType);
        return myParam;
    }
}