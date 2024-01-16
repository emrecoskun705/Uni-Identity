using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles;
using UniIdentity.Domain.Roles.ValueObjects;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(
            c => c.Value,
            val => new RoleId(val)
        );

        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .HasConversion(
                c => c.Value,
                val => new Name(val)
            );
        
        builder.HasIndex(x => x.Name)
            .HasDatabaseName("IX_Role_Name")
            .IsUnique();
    }
}