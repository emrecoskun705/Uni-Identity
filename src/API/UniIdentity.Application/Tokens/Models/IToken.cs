namespace UniIdentity.Application.Tokens.Models;

public interface IToken
{
    TokenType GetTokenType();
}