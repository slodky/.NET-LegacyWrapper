using System;
using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;

namespace LegacyWrapper.Client.Transport
{
    internal interface IWrapperClient : IDisposable
    {
        CallResult SendCallRequest(CallData callData);
    }
}