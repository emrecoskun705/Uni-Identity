using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Roles;

public sealed class RoleGraph : BaseEntity
{
    public RoleId ParentRoleId { get; private set; }
    public RoleId ChildRoleId { get; private set; }
    
    private RoleGraph(RoleId parentRoleId, RoleId childRoleId)
    {
        ParentRoleId = parentRoleId;
        ChildRoleId = childRoleId;
    }

    public static RoleGraph Create(RoleId parentRoleId, RoleId childRoleId)
    {
        return new(parentRoleId, childRoleId);
    }
}