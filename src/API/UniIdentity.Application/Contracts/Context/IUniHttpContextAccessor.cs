using Microsoft.AspNetCore.Http;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Application.Contracts.Context;

/// <summary>
/// Represents an accessor for accessing HTTP context information in a UniIdentity application.
/// </summary>
public interface IUniHttpContextAccessor
{
    /// <summary>
    /// Gets the HTTP context associated with the current request.
    /// </summary>
    HttpContext HttpContext { get; }

    /// <summary>
    /// Gets or sets the client identifier (client_id) associated with the OAuth 2.0 or OpenID Connect request.
    /// </summary>
    ClientKey? ClientId { get; }

    /// <summary>
    /// Gets or sets the realm identifier (realm_id) associated with the OAuth 2.0 or OpenID Connect request.
    /// </summary>
    RealmId? RealmId { get; }

    /// <summary>
    /// Retrieves the attribute associated with the specified name for the client.
    /// </summary>
    /// <param name="attributeName">The name of the attribute to retrieve.</param>
    /// <returns>
    /// The client attribute associated with the specified name, or null if not found.
    /// </returns>
    Task<ClientAttribute?> GetClientAttributeAsync(string attributeName);
}