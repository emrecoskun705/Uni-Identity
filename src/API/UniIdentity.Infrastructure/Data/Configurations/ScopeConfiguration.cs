using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ScopeConfiguration : IEntityTypeConfiguration<Scope>
{
    public void Configure(EntityTypeBuilder<Scope> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => ScopeId.FromValue(x));
                
        builder.Property(x => x.Name)
            .HasMaxLength(100);
        
        builder.Property(x => x.Protocol)
            .HasMaxLength(255);

        builder.Property(x => x.Description)
            .HasMaxLength(255);
        
        builder.Property(x => x.RealmId)
            .HasMaxLength(100)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));

        builder.HasIndex(x => new { x.RealmId, x.Name })
            .HasDatabaseName("IX_Scope_RealmId_Name")
            .IsUnique();
        
        builder.HasIndex(x => x.RealmId)
            .HasDatabaseName("IX_Scope_RealmId");

    }
}