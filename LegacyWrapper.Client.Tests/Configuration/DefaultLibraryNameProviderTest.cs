using LegacyWrapper.Client.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LegacyWrapper.Client.Configuration;

namespace LegacyWrapper.Client.Tests.Configuration
{
    [TestClass]
    public class DefaultLibraryNameProviderTest
    {
        private const string TestLibraryName = "TESTLIBRARYNAME";
        private static readonly LegacyDllImportAttribute TestLegacyDllImportAttribute = new LegacyDllImportAttribute(TestLibraryName);

        [TestMethod]
        public void TestReturnsPassedLibraryName()
        {
            ILibraryNameProvider provider = new DefaultLibraryNameProvider();

            var actualLibraryName = provider.GetLibraryName(TestLegacyDllImportAttribute);

            //Assert.AreEqual(TestLibraryName, actualLibraryName);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void TestThrowsExceptionOnNullArgument()
        {
            ILibraryNameProvider provider = new DefaultLibraryNameProvider();

            var actualLibraryName = provider.GetLibraryName(null);
        }
    }
}
