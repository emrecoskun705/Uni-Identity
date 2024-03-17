using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Roles;

/// <summary>
/// Represents a relationship between two roles in a role hierarchy.
/// </summary>
/// <remarks>
/// The <see cref="RoleGraph"/> class defines a relationship between a parent role and a child role within a role hierarchy. Instances of this class represent the structure of roles and their hierarchical relationships.
/// </remarks>
public sealed class RoleGraph : BaseEntity
{
    /// <summary>
    /// Gets the identifier of the parent role in the role hierarchy.
    /// </summary>
    public RoleId ParentRoleId { get; init; }
    
    /// <summary>
    /// Gets the identifier of the child role in the role hierarchy.
    /// </summary>
    public RoleId ChildRoleId { get; init; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleGraph"/> class with the specified parent and child role identifiers.
    /// </summary>
    /// <param name="parentRoleId">The identifier of the parent role.</param>
    /// <param name="childRoleId">The identifier of the child role.</param>
    public RoleGraph(RoleId parentRoleId, RoleId childRoleId)
    {
        ParentRoleId = parentRoleId;
        ChildRoleId = childRoleId;
    }
    
}