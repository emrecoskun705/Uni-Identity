using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Enums;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class RealmConfiguration : IEntityTypeConfiguration<Realm>
{
    public void Configure(EntityTypeBuilder<Realm> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasMaxLength(100)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));

        builder.Property(x => x.Name)
            .HasMaxLength(100);
        
        builder.Property(x => x.SslRequirement)
            .HasMaxLength(30)
            .HasConversion(
                x => x.ToString(),
                x => (SslRequirement)Enum.Parse(typeof(SslRequirement), x));

        builder.Property(x => x.Enabled)
            .HasDefaultValue(true);
        
        builder.Property(x => x.VerifyEmail)
            .HasDefaultValue(false);
    }
}