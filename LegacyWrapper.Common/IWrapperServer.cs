using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;

namespace LegacyWrapper.Common
{
    public interface IWrapperServer
    {
        CallResult InvokeFunction(CallData callData);
    }
}