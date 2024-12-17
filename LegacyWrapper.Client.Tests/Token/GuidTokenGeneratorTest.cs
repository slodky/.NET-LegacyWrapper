using System.Collections;
using LegacyWrapper.Client.Token;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LegacyWrapper.Client.Tests.Token
{
    [TestClass]
    public class GuidTokenGeneratorTest
    {
        [TestMethod]
        public void TestCreatesGuid()
        {
            ITokenGenerator generator = new GuidTokenGenerator();

            var token = generator.GenerateToken();

            var result = Guid.Parse(token.Token);
            Assert.AreEqual(token.Token, result.ToString());
        }

        [TestMethod]
        public void TestCreatesUniqueTokens()
        {
            ITokenGenerator generator = new GuidTokenGenerator();

            ICollection tokens = Enumerable.Range(1, 100)
                .Select(i => generator.GenerateToken())
                .ToList();

            CollectionAssert.AllItemsAreUnique(tokens);
        }
    }
}
