using LegacyWrapper.Common.Token;

namespace LegacyWrapper.Client.Token
{
    internal interface ITokenGenerator
    {
        PipeToken GenerateToken();
    }
}
