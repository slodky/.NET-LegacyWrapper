using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using LegacyWrapper.Common.Serialization.SystemText;

namespace LegacyWrapper.Common.Serialization
{
    /// <summary>
    /// Class to transmit info to the server. The server will execute an appropriate call and eventually return the results.
    /// </summary>
    [Serializable]
    public class CallData
    {
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
        
        /*public object ChangeType(object value, Type type)
        {
            if (value == null && type.IsGenericType) return Activator.CreateInstance(type);
            if (value == null) return null;
            if (type == value.GetType()) return value;
            if (type.IsEnum)
            {
                if (value is string)
                    return Enum.Parse(type, value as string);
                else
                    return Enum.ToObject(type, value);
            }
            if (!type.IsInterface && type.IsGenericType)
            {
                Type innerType = type.GetGenericArguments()[0];
                object innerValue = ChangeType(value, innerType);
                return Activator.CreateInstance(type, new object[] { innerValue });
            }
            if (value is string valueString && type == typeof(Byte[])) return Encoding.UTF8.GetBytes(valueString);
            if (value is string && type == typeof(Guid)) return new Guid(value as string);
            if (value is string && type == typeof(Version)) return new Version(value as string);
            if (!(value is IConvertible)) return value;
            return Convert.ChangeType(value, type);
        }*/
    }

    public enum KeepAliveStatus
    {
        KeepAlive,
        Close
    }
    
    

}
