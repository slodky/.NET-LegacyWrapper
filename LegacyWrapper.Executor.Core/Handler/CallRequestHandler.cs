using System;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;
using LegacyWrapper.Executor.Core.Interop;

namespace LegacyWrapper.Executor.Core.Handler
{
    internal class CallRequestHandler : ICallRequestHandler
    {
        public CallResult InvokeFunction(CallData callData)
        {
            CallResult result;
            try
            {
                result = UnmanagedLibraryLoader.InvokeUnmanagedFunction(callData);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error invoking function: {0}", e.ToString());

                return WriteExceptionToClient(e);
            }
            return result;
        }

        private CallResult WriteExceptionToClient(Exception e)
        {
            var callResult = new CallResult
            {
                //ExceptionMsg = e.Message
                //Exception = new LegacyWrapperException(errorMessage, e)
            };

            return callResult;
        }
    }
}
