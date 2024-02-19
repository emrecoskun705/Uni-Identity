using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.ValueObjects;
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
            .HasMaxLength(50)
            .HasConversion(
                c => c.Value,
                val => new Name(val)
            );

        builder.Property(x => x.ClientRealmConstraint)
            .HasMaxLength(100);

        builder.Property(x => x.RealmId)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));
        
        builder.Property(x => x.ClientId)
            .HasConversion(
                x => x.Value,
                x => new ClientId(x));
        
        builder.HasOne(x => x.Realm)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.RealmId);
        
        builder.HasOne(x => x.Client)
            .WithMany(x => x.Roles)
            .HasForeignKey(x => x.ClientId);
        
        builder.HasIndex(x => x.RealmId)
            .HasDatabaseName("IX_Role_RealmId");
        
        builder.HasIndex(x => x.ClientId)
            .HasDatabaseName("IX_Role_ClientId");
        
        builder.HasIndex(x => new { x.ClientRealmConstraint, x.Name})
            .HasDatabaseName("IX_Role_ClientRealmConstraint_Name")
            .IsUnique();
    }
}