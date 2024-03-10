using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Application.Tokens.Contracts;
using UniIdentity.Application.Tokens.Models;

namespace UniIdentity.Infrastructure.Tokens;

internal sealed class TokenBuilder : ITokenBuilder
{
    private readonly IUniHttpContext _httpContext;

    public TokenBuilder(IUniHttpContext httpContext)
    {
        _httpContext = httpContext;
    }

    public async Task<string> Create(IToken token)
    {
        var realm = await _httpContext.GetRealmAsync();
        
        var claims = token.GetClaims();
        
        var signatureAlgorithm = realm.GetSignatureAlgorithm();
        
        SecurityKey key;
        SigningCredentials signingCredentials;
        
        if (signatureAlgorithm.StartsWith("HS")) // HMAC algorithms
        {
            var hmacGenerationConfig = await _httpContext.GetHmacGenerationConfigAsync("default");
            
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hmacGenerationConfig.GetSecret()));
            key = symmetricSecurityKey;
            signingCredentials = new SigningCredentials(key, signatureAlgorithm);
        }
        else if (signatureAlgorithm.StartsWith("RS")) // RSA algorithms
        {
            var rsaGenerationConfig = await _httpContext.GetRsaGenerationConfigAsync("default");
            key = new RsaSecurityKey(rsaGenerationConfig.GetRsaParameters());
            signingCredentials = new SigningCredentials(key, signatureAlgorithm);
        }
        else
        {
            throw new ArgumentException("Unsupported security algorithm");
        }
        
        var tokenGenerator = new JwtSecurityToken
        (
            claims: claims,
            signingCredentials: signingCredentials
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(tokenGenerator);
    }
}