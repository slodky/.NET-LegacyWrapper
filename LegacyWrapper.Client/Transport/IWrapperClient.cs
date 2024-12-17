using System;
using LegacyWrapper.Common.Serialization;

namespace LegacyWrapper.Client.Transport
{
    internal interface IWrapperClient : IDisposable
    {
        CallResult SendCallRequest(CallData callData);
    }
}