using System.Collections.Generic;
using LegacyWrapper.Client.Architecture;
using PommaLabs.Thrower;

namespace LegacyWrapper.Client.Configuration
{
    internal class DefaultWrapperExecutableNameProvider : IWrapperExecutableNameProvider
    {
        private readonly IReadOnlyDictionary<TargetArchitecture, string> _wrapperNames = new Dictionary<TargetArchitecture, string>
        {
            { TargetArchitecture.X86,   "LegacyWrapper.Executor.x86.exe" },
            { TargetArchitecture.X64, "LegacyWrapper.Executor.x64.exe" },
        };

        private readonly IWrapperConfig _configuration;

        public DefaultWrapperExecutableNameProvider(IWrapperConfig configuration)
        {
            Raise.ArgumentNullException.IfIsNull(configuration);

            _configuration = configuration;
        }

        public string GetWrapperExecutableName()
        {
            return _wrapperNames[_configuration.TargetArchitecture];
        }
    }
}
