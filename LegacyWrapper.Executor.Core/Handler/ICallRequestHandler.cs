using LegacyWrapper.Common.Models;
using LegacyWrapper.Common.Serialization;

namespace LegacyWrapper.Executor.Core.Handler
{
    public interface ICallRequestHandler
    {
        /// <summary>
        /// Outsourced main method of the legacy dll wrapper.
        /// </summary>
        //void Call();

        CallResult InvokeFunction(CallData callData);
    }
}
