using System;
using System.Text.Json;
using LegacyWrapper.Common;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;
using LegacyWrapper.Common.Serialization.SystemText;
using LegacyWrapper.Executor.Core.Handler;
using NetMQ;
using NetMQ.Sockets;

namespace LegacyWrapper.Executor.Core.Transport;

public class ZeroMqServer : IWrapperServer
{
    private readonly ICallRequestHandler _callRequestHandler = new CallRequestHandler();
    private readonly IWrapperSerializer _serializer = new SystemJsonSerializer();
    
    private ResponseSocket _server;
    private bool _shouldClose = false;

    public ZeroMqServer()
    {
        _server = new ResponseSocket("@tcp://localhost:55567");
    }

    public void Run()
    {
        while (_shouldClose == false)
        {
            var received = _server.ReceiveFrameString();
            //Console.WriteLine("Received: {0}", received);
            
            var callData = _serializer.Deserialize<CallData>(received);
            var modelHelper = new ModelHelper();
            callData.Parameters = modelHelper.EnsureSerializationTypes(callData.Parameters, callData.ParameterTypes);
            
            var result = InvokeFunction(callData);
            
            //Console.WriteLine("Function invoked: {0}", callData.ProcedureName);

            var response = _serializer.Serialize<CallResult>(result);
            //Console.WriteLine("Sending: {0}", response);
            _server.SendFrame(response);
        }
    }

    public CallResult InvokeFunction(CallData callData)
    {
        _shouldClose = callData.Status == KeepAliveStatus.Close;

        return _callRequestHandler.InvokeFunction(callData);
    }
    
    private void CloseServer()
    {
        _server.Dispose();
    }
    
    #region IDisposable-Implementation
    
    private bool _isDisposed = false;
    
    ~ZeroMqServer()
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
            CloseServer();
        }
        // Free any unmanaged objects here.
        _server = null;
        _isDisposed = true;
    }
    #endregion
}