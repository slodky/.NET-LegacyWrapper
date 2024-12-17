using System.Diagnostics;
using PommaLabs.Thrower;

namespace LegacyWrapper.Client.ProcessHandling
{
    internal class ProcessFactory : IProcessFactory
    {
        public virtual MockableProcess GetProcess(string executableName, string args)
        {
            Raise.ArgumentNullException.IfIsNull(executableName, nameof(executableName));
            Raise.ArgumentNullException.IfIsNull(args, nameof(args));

            var process = new MockableProcess();
            process.StartInfo = new ProcessStartInfo(executableName, args);

            return process;
        }
    }
}
