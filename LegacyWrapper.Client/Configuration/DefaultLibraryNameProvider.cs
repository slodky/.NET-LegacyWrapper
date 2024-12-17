using LegacyWrapper.Client.Attributes;
using PommaLabs.Thrower;

namespace LegacyWrapper.Client.Configuration
{
    internal class DefaultLibraryNameProvider : ILibraryNameProvider
    {
        public string GetLibraryName(LegacyDllImportAttribute attribute)
        {
            Raise.ArgumentNullException.IfIsNull(attribute, nameof(attribute));

            return attribute.LibraryName;
        }
    }
}
