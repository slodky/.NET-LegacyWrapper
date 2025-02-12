using System;
using System.Runtime.Serialization;

namespace LegacyWrapper.Common.ErrorHandling
{
    [Serializable]
    public class LegacyWrapperException : Exception
    {

        /// <summary>
        /// Creates a new instance of <see cref="LegacyWrapperException"/>.
        /// </summary>
        public LegacyWrapperException()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="LegacyWrapperException"/> with the specified error message.
        /// </summary>
        /// <param name="message">Error message.</param>
        public LegacyWrapperException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="LegacyWrapperException"/> with the specified error message and inner exception.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="innerException">Inner exception.</param>
        public LegacyWrapperException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
