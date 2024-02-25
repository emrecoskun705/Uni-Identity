using System.Security.Claims;

namespace UniIdentity.Application.Tokens.Models;

public interface IToken
{
    TokenType GetTokenType();

    IEnumerable<Claim> GetClaims();
}