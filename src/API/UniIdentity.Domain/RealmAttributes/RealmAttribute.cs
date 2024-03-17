using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.RealmAttributes;

/// <summary>
/// Represents an attribute associated with a realm entity.
/// </summary>
/// <remarks>
/// Realm attributes provide additional information or metadata associated with a realm within the UniIdentity domain. These attributes typically consist of a name-value pair and are used to customize realm behavior or configuration.
/// </remarks>
public class RealmAttribute : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier of the realm attribute.
    /// </summary>
    public RealmId Id { get; private set; }

    /// <summary>
    /// Gets the name of the realm attribute.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets the value of the realm attribute.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="RealmAttribute"/> class with the specified parameters.
    /// </summary>
    /// <param name="id">The unique identifier of the realm attribute.</param>
    /// <param name="name">The name of the realm attribute.</param>
    /// <param name="value">The value of the realm attribute.</param>
    public RealmAttribute(
        RealmId id,
        string name,
        string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
    
}