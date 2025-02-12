using System;
using LegacyWrapper.Client.Transport;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;
using PommaLabs.Thrower;

namespace LegacyWrapper.Client.Client
{
    internal class WrapperInvoker : IDisposable
    {
        private readonly IWrapperClient _wrapperClient;

        private bool _isDisposed = false;

        public WrapperInvoker(IWrapperClient wrapperClient)
        {
            Raise.ArgumentNullException.IfIsNull(wrapperClient, nameof(wrapperClient));

            _wrapperClient = wrapperClient;
        }

        /// <summary>
        /// Executes a call to a library.
        /// </summary>
        /// <param name="callData"><see cref="CallData">CallData</see> object with information about invocation.</param>
        /// <returns>Result object returned by the library.</returns>
        /// <exception cref="Exception">This Method will rethrow all exceptions thrown by the wrapper.</exception>
        protected internal virtual object InvokeInternal(CallData callData)
        {
            var callResult = _wrapperClient.SendCallRequest(callData);

            CopyParameters(callResult, callData);

            return callResult;
        }

        private void CopyParameters(CallResult callResult, CallData callData)
        {
            string errorMessage = "Returned parameters differ in length from passed parameters";
            Raise.InvalidDataException.If(callData.Parameters.Length != callResult.Parameters.Length, errorMessage);

            Array.Copy(callResult.Parameters, callData.Parameters, callResult.Parameters.Length);
        }

        #region IDisposable-Implementation
        ~WrapperInvoker()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                _wrapperClient.Dispose();
            }

            // Free any unmanaged objects here.

            _isDisposed = true;
        }
        #endregion

    }
}
