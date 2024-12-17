using LegacyWrapper.Client.ProcessHandling;
using LegacyWrapper.Common.ErrorHandling;
using LegacyWrapper.Common.Serialization;
using LegacyWrapper.Common.Serialization.SystemText;
using LegacyWrapper.Common.Standard.Models;
using NetMQ;
using NetMQ.Sockets;
using PommaLabs.Thrower;

namespace LegacyWrapper.Client.Transport
{
    internal class ZeroMqClient : IWrapperClient
    {
        private bool _isDisposed;

        private readonly IWrapperProcessStarter _wrapperProcessStarter;
        private readonly IWrapperSerializer _serializer = new SystemJsonSerializer();

        private readonly RequestSocket _client;

        public ZeroMqClient(IWrapperProcessStarter wrapperProcessStarter)
        {
            Raise.ArgumentNullException.IfIsNull(wrapperProcessStarter, nameof(wrapperProcessStarter));
            
            _wrapperProcessStarter = wrapperProcessStarter;
            _wrapperProcessStarter.StartWrapperProcess(); 

            _client = new RequestSocket();
            _client.Connect("tcp://localhost:55567");
        }
        
        public void Dispose()
        {
            _client.Dispose();
        }

        public CallResult SendCallRequest(CallData callData)
        {
            var serialized = _serializer.Serialize(callData);

            _client.SendFrame(serialized);
            
            var response = _client.ReceiveFrameString();
            
            //Console.WriteLine(response);
            
            var result = _serializer.Deserialize<CallResult>(response);
            
            ThrowIfError(result);
            
            var modelHelper = new ModelHelper();
            result.Parameters = modelHelper.EnsureSerializationTypes(result.Parameters, callData.ParameterTypes);
            
            return result;
        }

        private static void ThrowIfError(CallResult callResult)
        {
            if (string.IsNullOrEmpty(callResult.ExceptionMsg) == false)
            {
                throw new LegacyWrapperException(callResult.ExceptionMsg);
            } 
        }
    }
}