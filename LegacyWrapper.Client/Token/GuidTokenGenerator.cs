using System;
using LegacyWrapper.Common.Token;

namespace LegacyWrapper.Client.Token
{
    internal class GuidTokenGenerator : ITokenGenerator
    {
        public PipeToken GenerateToken()
        {
            string token = Guid.NewGuid().ToString();
            return new PipeToken(token);
        }
    }
}
