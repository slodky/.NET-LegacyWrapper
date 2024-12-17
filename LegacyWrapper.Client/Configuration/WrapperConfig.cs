using LegacyWrapper.Client.Architecture;

namespace LegacyWrapper.Client.Configuration
{
    internal class WrapperConfig : IWrapperConfig
    {
        public TargetArchitecture TargetArchitecture { get; set; }
    }
}
