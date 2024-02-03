namespace UniIdentity.Domain.Representation;

public interface IToken
{
    TokenType GetTokenType();
}