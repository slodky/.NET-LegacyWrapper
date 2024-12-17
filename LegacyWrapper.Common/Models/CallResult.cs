using System;
using System.Text.Json;
using LegacyWrapper.Common.Serialization.SystemText;

namespace LegacyWrapper.Common.Serialization
{
    /// <summary>
    /// Class used to return the call result back to the client. 
    /// Includes return value, eventually changed ref params, and if thrown in the wrapper, an exception.
    /// </summary>
    [Serializable]
    public class CallResult
    {
        private readonly IWrapperSerializer _serializer = new SystemJsonSerializer();
        
        /// <summary>
        /// Result object of the call.
        /// </summary>
        public object Result { get; set; }

        public object GetProperResult(Type resultType)
        {
            if (resultType == typeof(void))
            {
                return null;
            }
            if (Result is JsonElement myResultJsonElement)
            {
                if (myResultJsonElement.ValueKind == JsonValueKind.Null)
                {
                    return null;
                }
                return _serializer.Deserialize(Result.ToString(), resultType);
                //return myResultJsonElement.Deserialize(resultType);
            }
            return Convert.ChangeType(Result, resultType);
        }

        /// <summary>
        /// Array of parameters passed to the function call.
        /// The original parameters may have changed due to ref parameters used in the dll function.
        /// </summary>
        public object[] Parameters { get; set; }

        /// <summary>
        /// If the library call in the wrapper throws an exception, it will be delivered here.
        /// </summary>
        public string ExceptionMsg { get; set; }
    }
}
