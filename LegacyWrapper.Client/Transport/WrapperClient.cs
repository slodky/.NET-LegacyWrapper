/*
using System;
using System.IO;
using LegacyWrapper.Client.ProcessHandling;
using LegacyWrapper.Common.net8;
using LegacyWrapper.Common.net8.Serialization;
using PommaLabs.Thrower;
using ServiceWire.NamedPipes;

namespace LegacyWrapper.Client.Transport
{
    internal class WrapperClient : IWrapperClient
    {
        private bool _isDisposed;

        private readonly IWrapperProcessStarter _wrapperProcessStarter;
        private readonly NpClient<IWrapperServer> _client;

        /// <summary>
        /// Creates a new WrapperClient instance.
        /// </summary>
        /// <param name="wrapperProcessStarter">WrapperProcessStarter instance for invoking the appropriate wrapper executable.</param>
        public WrapperClient(IWrapperProcessStarter wrapperProcessStarter)
        {
            Raise.ArgumentNullException.IfIsNull(wrapperProcessStarter, nameof(wrapperProcessStarter));
            
            _wrapperProcessStarter = wrapperProcessStarter;


            _wrapperProcessStarter.StartWrapperProcess();

            _client = new NpClient<IWrapperServer>(
                new NpEndPoint(PipeConst.PipeName),
                new BetterSerializer());
        }

        public CallResult SendCallRequest(CallData callData)
        {
            CallResult callResult = null;
            try
            {
                callResult = _client.Proxy.InvokeFunction(callData);
            }
            catch (Exception)
            {
                ClosePipe();
                throw;
            }
            
            if (callResult.Exception != null)
            {
                ClosePipe();
                throw callResult.Exception;
            }
            return callResult;
        }

        private void ClosePipe()
        {
            CallData info = new CallData { Status = KeepAliveStatus.Close };

            try
            {
                if (_client.IsConnected)
                {
                    _client.Proxy.InvokeFunction(info);
                }
                _client.Dispose();
            }
            catch(IOException) { } // This means the wrapper eventually crashed and doesn't need a clean shutdown anyways

        }

        #region IDisposable-Implementation
        ~WrapperClient()
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
                ClosePipe();

                _wrapperProcessStarter.Dispose();
            }

            // Free any unmanaged objects here.
            _isDisposed = true;
        }
        #endregion

    }

}
*/
