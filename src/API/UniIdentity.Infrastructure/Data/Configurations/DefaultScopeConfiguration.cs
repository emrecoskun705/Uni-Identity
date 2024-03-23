using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class DefaultScopeConfiguration : IEntityTypeConfiguration<DefaultScope>
{
    public void Configure(EntityTypeBuilder<DefaultScope> builder)
    {
        builder.HasKey(x => new { x.RealmId, x.ScopeId });

        builder.Property(x => x.RealmId)
            .HasMaxLength(100)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));

        builder.Property(x => x.ScopeId)
            .HasConversion(
                x => x.Value,
                x => ScopeId.FromValue(x));

        builder.HasOne<Realm>()
            .WithMany()
            .HasForeignKey(x => x.RealmId);

        builder.HasOne<Scope>()
            .WithOne()
            .HasForeignKey<DefaultScope>(x => x.ScopeId);

        builder.HasIndex(x => x.ScopeId)
            .HasDatabaseName("IX_DefaultScope_ScopeId")
            .IsUnique(false);
        
        builder.HasIndex(x => x.RealmId)
            .HasDatabaseName("IX_DefaultScope_RealmId")
            .IsUnique(false);
    }
}