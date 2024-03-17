namespace UniIdentity.Domain.Clients.Enums;

/// <summary>
/// Specifies the access type of a client within the UniIdentity domain.
/// </summary>
/// <remarks>
/// The <see cref="AccessType"/> enumeration defines the access type of a client, indicating how the client interacts with the identity system. Clients can be categorized as confidential, public, or bearer-only, based on their authentication requirements and capabilities.
/// </remarks>
public enum AccessType
{
    /// <summary>
    /// Indicates that the client requires confidential access, typically requiring the exchange of credentials with the authorization server.
    /// </summary>
    Confidential,
    /// <summary>
    /// Indicates that the client allows public access, implying that no client credentials are required for authentication.
    /// </summary>
    Public,
    /// <summary>
    /// Indicates that the client only accepts bearer tokens for authentication, without supporting client credentials.
    /// </summary>
    BearerOnly,
}