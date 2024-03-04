using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ConfigConfiguration : IEntityTypeConfiguration<Config>
{
    public void Configure(EntityTypeBuilder<Config> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.RealmId, x.Name })
            .HasDatabaseName("IX_Config_RealmId_Name")
            .IsUnique();

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => new ConfigId(x));

        builder.Property(x => x.RealmId)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));

        builder.Property(x => x.ProviderType)
            .HasMaxLength(400)
            .HasConversion(
                x => x.Value,
                x => ProviderType.FromValue(x));
        
        builder.HasDiscriminator<ProviderType>("ProviderType")
            .HasValue<RsaGenerationConfig>(ProviderType.RsaKeyGen);

        builder.Property(x => x.Name)
            .HasMaxLength(150);
    }
}