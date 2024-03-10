
using UniIdentity.Application.Tokens.Models;

namespace UniIdentity.Application.Tokens.Contracts;

public interface ITokenBuilder
{
     Task<string> Create(IToken token);
}