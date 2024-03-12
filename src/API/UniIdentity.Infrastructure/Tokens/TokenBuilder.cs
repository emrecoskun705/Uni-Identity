using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Application.Tokens.Contracts;
using UniIdentity.Application.Tokens.Models;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Configs.Enums;
using UniIdentity.Domain.OIDC;
using UniIdentity.Domain.Realms;

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
        var client = await _httpContext.GetClientAsync();
        
        var claims = token.GetClaims();
        
        var signatureAlgorithm = GetSignatureAlgorithm(realm, client, token);
        
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
    
    /// <summary>
    /// Retrieves the signature algorithm to be used for generating tokens based on the realm, client, and token type.
    /// </summary>
    private static string GetSignatureAlgorithm(Realm realm, Client client, IToken token)
    {
        var algorithm = (token.GetTokenType() switch
        {
            TokenType.Access => client.GetAttribute(OIDCAttribute.AccessTokenAlgorithm),
            TokenType.Id => client.GetAttribute(OIDCAttribute.IdTokenAlgorithm),
            _ => throw new InvalidEnumArgumentException("Invalid TokenType enum exception.")
        } ?? realm.GetSignatureAlgorithm()) ?? SignatureAlg.Default;

        return algorithm;
    }
    
}