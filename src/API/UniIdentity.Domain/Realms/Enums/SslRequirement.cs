namespace UniIdentity.Domain.Realms.Enums;

/// <summary>
/// Represents the SSL requirement for communication within the UniIdentity domain.
/// </summary>
/// <remarks>
/// SSL requirement specifies the level of security protocol required for communication between entities within the UniIdentity domain. This enumeration defines different SSL requirements, including requiring SSL for all communication, requiring SSL for external communication only, or not requiring SSL at all.
/// </remarks>
public enum SslRequirement
{
    /// <summary>
    /// Requires SSL for all communication.
    /// </summary>
    All,

    /// <summary>
    /// Requires SSL for external communication only.
    /// </summary>
    External,

    /// <summary>
    /// Does not require SSL.
    /// </summary>
    None,
}
