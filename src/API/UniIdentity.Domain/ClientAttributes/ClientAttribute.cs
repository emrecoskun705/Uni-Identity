using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.ClientAttributes;

/// <summary>
/// Represents an attribute associated with a client entity.
/// </summary>
/// <remarks>
/// Client attributes provide additional information or metadata associated with a client within the UniIdentity domain. These attributes typically consist of a name-value pair and are used to customize client behavior or configuration.
/// </remarks>
public sealed class ClientAttribute : BaseEntity
{
    /// <summary>
    /// Gets the client id of the client associated with this attribute.
    /// </summary>
    public ClientId ClientId { get; private set; }
    
    /// <summary>
    /// Gets the name of the client attribute.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the value of the client attribute.
    /// </summary>
    public string Value { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientAttribute"/> class with the specified parameters.
    /// </summary>
    /// <param name="clientClientId">The unique identifier of the client associated with this attribute.</param>
    /// <param name="name">The name of the client attribute.</param>
    /// <param name="value">The value of the client attribute.</param>
    internal ClientAttribute(
        ClientId clientId,
        string name,
        string value)
    {
        ClientId = clientId;
        Name = name;
        Value = value;
    }
    
}