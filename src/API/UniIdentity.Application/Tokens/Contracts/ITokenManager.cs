using UniIdentity.Application.Tokens.Models;

namespace UniIdentity.Application.Tokens.Contracts;

public interface ITokenManager
{
    string Encode(IToken token);
}