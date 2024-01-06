using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Credentials;
using UniIdentity.Domain.Credentials.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class CredentialConfiguration : IEntityTypeConfiguration<Credential>
{
    public void Configure(EntityTypeBuilder<Credential> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => CredentialId.FromValue(x));

        builder.Property(x => x.UserId)
            .HasConversion(
                x => x.Value,
                x => UserId.FromValue(x));

        builder.Property(x => x.Type)
            .HasMaxLength(30)
            .HasConversion(
                x => x.Value,
                x => CredentialType.FromValue(x));

        builder.HasDiscriminator<CredentialType>("Type")
            .HasValue<PasswordCredential>(CredentialType.Password);
        
        builder.HasIndex(x => x.UserId)
            .HasDatabaseName("IX_Credential_UserId");

    }
}