using System;
using System.Runtime.InteropServices;

namespace LegacyWrapper.Client.Attributes
{
    /// <summary>
    /// Marks a method in an interface for usage with the WrapperClient.
    /// </summary>
    //[AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class LegacyDllMethodAttribute : Attribute
    {
        /// <summary>
        /// CallingConvention to use when calling a P/Invoke function.
        /// </summary>
        public CallingConvention CallingConvention { get; set; }

        /// <summary>
        /// Charset to use when calling a P/Invoke function.
        /// </summary>
        public CharSet CharSet { get; set; }

        /// <summary>
        /// Creates a new instance of LegacyDllMethodAttribute.
        /// </summary>
        public LegacyDllMethodAttribute()
        {
            CallingConvention = CallingConvention.StdCall;
            CharSet = CharSet.Auto;
        }
    }
}
