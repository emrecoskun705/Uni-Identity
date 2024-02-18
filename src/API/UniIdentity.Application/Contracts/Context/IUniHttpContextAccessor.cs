using Microsoft.AspNetCore.Http;

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
    string? ClientId { get; init; }

    /// <summary>
    /// Gets or sets the realm identifier (realm_id) associated with the OAuth 2.0 or OpenID Connect request.
    /// </summary>
    string? RealmId { get; init; }
}