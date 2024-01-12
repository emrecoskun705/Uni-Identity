using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ScopeAttributeConfiguration : IEntityTypeConfiguration<ScopeAttribute>
{
    public void Configure(EntityTypeBuilder<ScopeAttribute> builder)
    {
        builder.HasKey(x => new { x.Id, x.Name });

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => ScopeId.FromValue(x));
        
        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.Property(x => x.Value)
            .HasMaxLength(2000);
        
        builder.HasOne(x => x.Scope)
            .WithMany(x => x.ScopeAttributes)
            .HasForeignKey(x => x.Id);
    }
}