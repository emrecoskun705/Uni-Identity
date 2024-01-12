using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class RealmAttributeConfiguration : IEntityTypeConfiguration<RealmAttribute>
{
    public void Configure(EntityTypeBuilder<RealmAttribute> builder)
    {
        builder.HasKey(x => new { x.Id, x.Name });
        
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));
        
        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.Property(x => x.Value)
            .HasMaxLength(3000);
        
        builder.HasOne(x => x.Realm)
            .WithMany(x => x.RealmAttributes)
            .HasForeignKey(x => x.Id);
    }
}