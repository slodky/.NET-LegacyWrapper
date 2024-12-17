using LegacyWrapper.Client.Attributes;

namespace LegacyWrapper.Client.Configuration
{
    internal interface ILibraryNameProvider
    {
        string GetLibraryName(LegacyDllImportAttribute attribute);
    }
}
