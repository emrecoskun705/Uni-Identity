using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles.ValueObjects;

namespace UniIdentity.Domain.Roles;

/// <summary>
/// Represents a role within the UniIdentity domain, which defines a set of permissions granted to users or clients.
/// </summary>
public sealed class Role : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier of the role.
    /// </summary>
    public RoleId Id { get; init; }
    
    /// <summary>
    /// Gets the name of the role.
    /// </summary>
    public Name Name { get; private set; }
    
    /// <summary>
    /// Gets or sets the client-realm constraint associated with the role.
    /// </summary>
    /// <remarks>
    /// For client roles, this constraint specifies the client identifier. For realm roles, it specifies the realm identifier.
    /// </remarks>
    public string ClientRealmConstraint { get; init; }
    
    /// <summary>
    /// Gets a value indicating whether the role is a client-specific role.
    /// </summary>
    public bool IsClientRole { get; private set; }
    
    /// <summary>
    /// Gets the identifier of the realm to which the role belongs.
    /// </summary>
    public RealmId RealmId { get; private set; }
    
    /// <summary>
    /// Gets the identifier of the client associated with the role (if applicable).
    /// </summary>
    public ClientId? ClientId { get; private set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class representing a realm role.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <param name="name">The name of the role.</param>
    /// <param name="realmId">The identifier of the realm to which the role belongs.</param>
    private Role(RoleId id, Name name, RealmId realmId)
    {
        Id = id;
        Name = name;
        RealmId = realmId;
        ClientId = null;

        ClientRealmConstraint = realmId.Value;
        IsClientRole = false;
    }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Role"/> class representing a client role.
    /// </summary>
    /// <param name="id">The unique identifier of the role.</param>
    /// <param name="name">The name of the role.</param>
    /// <param name="realmId">The identifier of the realm to which the role belongs.</param>
    /// <param name="clientId">The identifier of the client associated with the role.</param>
    private Role(RoleId id, Name name, RealmId realmId, ClientId clientId)
    {
        Id = id;
        Name = name;
        RealmId = realmId;
        ClientId = clientId;

        ClientRealmConstraint = clientId.Value.ToString();
        IsClientRole = true;
    }
    
    /// <summary>
    /// Creates a new realm role with the specified name and realm identifier.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <param name="realmId">The identifier of the realm to which the role belongs.</param>
    /// <returns>A new instance of <see cref="Role"/> representing a realm role.</returns>
    public static Role CreateRealmRole(Name name, RealmId realmId)
    {
        var role = new Role(RoleId.New(), name, realmId);
        
        return role;
    }
    
    /// <summary>
    /// Creates a new realm role with the specified identifier, name, and realm identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="name">The name of the role.</param>
    /// <param name="realmId">The identifier of the realm to which the role belongs.</param>
    /// <returns>A new instance of <see cref="Role"/> representing a realm role.</returns>
    public static Role CreateRealmRole(RoleId roleId, Name name, RealmId realmId)
    {
        var role = new Role(roleId, name, realmId);

        return role;
    }
    
    /// <summary>
    /// Creates a new client role with the specified name, realm identifier, and client identifier.
    /// </summary>
    /// <param name="name">The name of the role.</param>
    /// <param name="realmId">The identifier of the realm to which the role belongs.</param>
    /// <param name="clientId">The identifier of the client associated with the role.</param>
    /// <returns>A new instance of <see cref="Role"/> representing a client role.</returns>

    public static Role CreateClientRole(Name name, RealmId realmId, ClientId clientId)
    {
        var role = new Role(RoleId.New(), name, realmId, clientId)
        {
            ClientRealmConstraint = clientId.Value.ToString(),
            IsClientRole = true
        };

        return role;
    }
    
    /// <summary>
    /// Creates a new client role with the specified identifier, name, realm identifier, and client identifier.
    /// </summary>
    /// <param name="roleId">The unique identifier of the role.</param>
    /// <param name="name">The name of the role.</param>
    /// <param name="realmId">The identifier of the realm to which the role belongs.</param>
    /// <param name="clientId">The identifier of the client associated with the role.</param>
    /// <returns>A new instance of <see cref="Role"/> representing a client role.</returns>
    public static Role CreateClientRole(RoleId roleId, Name name, RealmId realmId, ClientId clientId)
    {
        var role = new Role(roleId, name, realmId, clientId)
        {
            ClientRealmConstraint = clientId.Value.ToString(),
            IsClientRole = true
        };

        return role;
    }
}