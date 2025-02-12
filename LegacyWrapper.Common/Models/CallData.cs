using System;
using System.Runtime.InteropServices;
using LegacyWrapper.Common.Serialization;
using LegacyWrapper.Common.Serialization.SystemText;

namespace LegacyWrapper.Common.Models
{
    /// <summary>
    /// Class to transmit info to the server. The server will execute an appropriate call and eventually return the results.
    /// </summary>
    [Serializable]
    public class CallData
    {
        private readonly IWrapperSerializer _serializer = new SystemJsonSerializer();

        public CallData()
        {
            Status = KeepAliveStatus.KeepAlive;
        }

        public string LibraryName { get; set; }

        /// <summary>
        /// Name of the procedure to call.
        /// </summary>
        public string ProcedureName { get; set; }
        
        /// <summary>
        /// Array of parameters to pass to the function call.
        /// </summary>
        public object[] Parameters { get; set; }

        public object[] GetProperParameters()
        {
            var result = new object[Parameters.Length];
            for (var index = 0; index < Parameters.Length; index++)
            {
                var properType = ParameterTypes[index];
                result[index] = _serializer.Deserialize(Parameters[index].ToString(), properType);

            }

            return result;
        }
        
        public Type[] ParameterTypes { get; set; }

        /// <summary>
        /// Type of the called function's return value
        /// </summary>
        public Type? ReturnType { get; set; }

        public CallingConvention CallingConvention { get; set; }

        public CharSet CharSet { get; set; }

        /// <summary>
        /// Status indicating if the wrapper executable should close the connection and terminate itself
        /// </summary>
        public KeepAliveStatus Status { get; set; }
    }

    public enum KeepAliveStatus
    {
        KeepAlive,
        Close
    }
}
