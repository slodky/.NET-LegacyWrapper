using System;
using LegacyWrapper.Common.Attributes;
using LegacyWrapper.Client.Configuration;

namespace LegacyWrapperTest.Integration.TestSetup
{
    class TestLibraryNameProvider : ILibraryNameProvider
    {
        public string GetLibraryName(LegacyDllImportAttribute attribute)
        {
            if (Environment.Is64BitProcess)
            {
                return @"TestLibrary\LegacyWrapperTestDll32.dll";
            }
            else
            {
                return @"TestLibrary\LegacyWrapperTestDll64.dll";
            }
        }
    }
}
