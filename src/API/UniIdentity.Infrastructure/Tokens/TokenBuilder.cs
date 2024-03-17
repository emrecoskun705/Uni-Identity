using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Application.Tokens.Contracts;
using UniIdentity.Application.Tokens.Models;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Configs.Enums;
using UniIdentity.Domain.OIDC;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Tokens;

internal sealed class TokenBuilder : ITokenBuilder
{
    private readonly IUniHttpContext _httpContext;
    private readonly IGetRealmAttributeRepository _getRealmAttributeRepository;
    private readonly IGetClientAttributeRepository _getClientAttributeRepository;
    
    public TokenBuilder(IUniHttpContext httpContext, IGetRealmAttributeRepository getRealmAttributeRepository, IGetClientAttributeRepository getClientAttributeRepository)
    {
        _httpContext = httpContext;
        _getRealmAttributeRepository = getRealmAttributeRepository;
        _getClientAttributeRepository = getClientAttributeRepository;
    }

    public async Task<string> Create(IToken token, CancellationToken cancellationToken = default)
    {
        var realm = await _httpContext.GetRealmAsync(cancellationToken);
        var client = await _httpContext.GetClientAsync(cancellationToken);
        
        var claims = token.GetClaims();
        
        var signatureAlgorithm = await GetSignatureAlgorithm(realm, client, token, cancellationToken);
        
        SecurityKey key;
        SigningCredentials signingCredentials;
        
        if (signatureAlgorithm.StartsWith("HS")) // HMAC algorithms
        {
            var hmacGenerationConfig = await _httpContext.GetHmacGenerationConfigAsync("default", cancellationToken);
            
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(hmacGenerationConfig.GetSecret()));
            key = symmetricSecurityKey;
            signingCredentials = new SigningCredentials(key, signatureAlgorithm);
        }
        else if (signatureAlgorithm.StartsWith("RS")) // RSA algorithms
        {
            var rsaGenerationConfig = await _httpContext.GetRsaGenerationConfigAsync("default", cancellationToken);
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
    private async Task<string> GetSignatureAlgorithm(Realm realm, Client client, IToken token, CancellationToken cancellationToken = default)
    {
        var algorithm = (token.GetTokenType() switch
        {
            TokenType.Access => await client.GetAttribute(OIDCAttribute.AccessTokenAlgorithm, _getClientAttributeRepository),
            TokenType.Id => await client.GetAttribute(OIDCAttribute.IdTokenAlgorithm, _getClientAttributeRepository),
            _ => throw new InvalidEnumArgumentException("Invalid TokenType enum exception.")
        } ?? await realm.GetSignatureAlgorithm(_getRealmAttributeRepository, cancellationToken)) ?? SignatureAlg.Default;

        return algorithm;
    }
    
}