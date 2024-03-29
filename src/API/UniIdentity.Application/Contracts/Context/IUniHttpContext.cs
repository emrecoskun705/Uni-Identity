﻿using Microsoft.AspNetCore.Http;
using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Application.Contracts.Context;

/// <summary>
/// Represents an accessor for accessing HTTP context information in a UniIdentity application.
/// </summary>
public interface IUniHttpContext
{
    /// <summary>
    /// Gets the HTTP context associated with the current request.
    /// </summary>
    HttpContext HttpContext { get; }

    /// <summary>
    /// Gets or sets the client identifier (client_id) associated with the OAuth 2.0 or OpenID Connect request.
    /// </summary>
    ClientKey? ClientKey { get; }

    /// <summary>
    /// Gets or sets the realm identifier (realm_id) associated with the OAuth 2.0 or OpenID Connect request.
    /// </summary>
    RealmId RealmId { get; }

    /// <summary>
    /// Retrieves the attribute associated with the specified name for the client.
    /// </summary>
    /// <param name="attributeName">The name of the attribute to retrieve.</param>
    /// <returns>
    /// The client attribute associated with the specified name, or null if not found.
    /// </returns>
    Task<ClientAttribute?> GetClientAttributeAsync(string attributeName);

    /// <summary>
    /// Retrieves the realm asynchronously.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The task result contains the realm.</returns>
    Task<Realm> GetRealmAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieves the client asynchronously.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The task result contains the client.</returns>
    Task<Client> GetClientAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieves the RSA generation configuration asynchronously for the specified name.
    /// </summary>
    /// <param name="name">The name of the RSA generation configuration to retrieve.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The task result contains the RSA generation configuration.</returns>
    Task<RsaGenerationConfig> GetRsaGenerationConfigAsync(string name, CancellationToken ct = default);
    
    /// <summary>
    /// Retrieves the HMAC generation configuration asynchronously for the specified name.
    /// </summary>
    /// <param name="name">The name of the HMAC generation configuration to retrieve.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The task result contains the HMAC generation configuration.</returns>
    Task<HmacGenerationConfig> GetHmacGenerationConfigAsync(string name, CancellationToken ct = default);
}