using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Configs;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ConfigAttributeConfiguration : IEntityTypeConfiguration<ConfigAttribute>
{
    public void Configure(EntityTypeBuilder<ConfigAttribute> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => new ConfigAttributeId(x));
        
        builder.Property(x => x.ConfigId)
            .HasConversion(
                x => x.Value,
                x => new ConfigId(x));
        
        builder.HasIndex(x => x.ConfigId);

        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.Property(x => x.Value)
            .HasMaxLength(3000);

        builder.HasOne(x => x.Config)
            .WithMany(x => x.ConfigAttributes)
            .HasForeignKey(x => x.ConfigId);
    }
}