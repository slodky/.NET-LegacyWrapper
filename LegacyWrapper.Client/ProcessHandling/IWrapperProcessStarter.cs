using System;

namespace LegacyWrapper.Client.ProcessHandling
{
    internal interface IWrapperProcessStarter : IDisposable
    {
        void StartWrapperProcess();
    }
}
