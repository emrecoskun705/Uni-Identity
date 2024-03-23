using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes;

/// <summary>
/// Represents a scope within a realm, which defines a specific set of access rights and privileges for a client.
/// </summary>
public sealed class Scope : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier of the scope.
    /// </summary>
    public ScopeId Id { get; private set; }
    
    /// <summary>
    /// Gets the name of the scope.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the protocol associated with the scope.
    /// </summary>
    public string Protocol { get; private set; }
    
    /// <summary>
    /// Gets the unique identifier of the realm to which the scope belongs.
    /// </summary>
    public RealmId RealmId { get; private set; }
    
    /// <summary>
    /// Gets the description of the scope.
    /// </summary>
    public string Description { get; private set; }

    private Scope(ScopeId id, string name, string protocol, RealmId realmId, string description)
    {
        Id = id;
        Name = name;
        Protocol = protocol;
        RealmId = realmId;
        RealmId = realmId;
        Description = description;
    }

    /// <summary>
    /// Creates a new scope with the specified attributes.
    /// </summary>
    /// <param name="name">The name of the scope.</param>
    /// <param name="protocol">The protocol associated with the scope.</param>
    /// <param name="realmId">The unique identifier of the realm to which the scope belongs.</param>
    /// <param name="description">The description of the scope.</param>
    /// <returns>A new instance of the <see cref="Scope"/> class.</returns>
    public static Scope Create(string name, string protocol, RealmId realmId, string description)
    {
        return new Scope(ScopeId.New(), name, protocol, realmId, description);
    }
    
    
}