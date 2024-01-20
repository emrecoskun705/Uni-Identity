using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Roles;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class RoleGraphConfiguration : IEntityTypeConfiguration<RoleGraph>
{
    public void Configure(EntityTypeBuilder<RoleGraph> builder)
    {
        builder.HasKey(x => new { x.ParentRoleId, x.ChildRoleId});
        builder.Property(x => x.ParentRoleId)
            .HasConversion(
                x => x.Value,
                x => new RoleId(x));
        
        builder.Property(x => x.ChildRoleId)
            .HasConversion(
                x => x.Value,
                x => new RoleId(x));

        builder.HasOne(x => x.ParentRole)
            .WithMany(x => x.ParentRoles)
            .HasForeignKey(x => x.ParentRoleId);
        
        builder.HasOne(x => x.ChildRole)
            .WithMany(x => x.ChildRoles)
            .HasForeignKey(x => x.ChildRoleId);

        builder.HasIndex(x => x.ParentRoleId)
            .HasDatabaseName("IX_RoleGraph_ParentRoleId");
        
        builder.HasIndex(x => x.ChildRoleId)
            .HasDatabaseName("IX_RoleGraph_ChildRoleId");
    }
}