using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Enums;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                x => ClientId.FromValue(x));

        builder.Property(x => x.ClientId)
            .HasMaxLength(255);

        builder.Property(x => x.ClientSecret)
            .HasMaxLength(255);

        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.Property(x => x.Protocol)
            .HasMaxLength(30)
            .HasConversion(
                x => x.Value,
                x => Protocol.FromValue(x));

        builder.Property(x => x.BaseUrl)
            .HasMaxLength(255);

        builder.Property(x => x.RootUrl)
            .HasMaxLength(255);

        builder.Property(x => x.ManagementUrl)
            .HasMaxLength(255);

        builder.Property(x => x.ClientAuthenticationType)
            .HasMaxLength(50)
            .HasConversion(
                x => x.Value,
                x => ClientAuthenticationType.FromValue(x));

        builder.Property(x => x.RegistrationToken)
            .HasMaxLength(255);

        builder.Property(x => x.AccessType)
            .HasMaxLength(20)
            .HasConversion(
                x => x.ToString(),
                x => (AccessType)Enum.Parse(typeof(AccessType), x));

        builder.Property(x => x.RealmId)
            .HasConversion(
                x => x.Value,
                x => new RealmId(x));

        builder.Property(x => x.PublicClient)
            .HasDefaultValue(false);
        
        builder.Property(x => x.Enabled)
            .HasDefaultValue(false);
        
        builder.Property(x => x.BearerOnly)
            .HasDefaultValue(false);
        
        builder.Property(x => x.ConsentRequired)
            .HasDefaultValue(false);
        
        builder.Property(x => x.AuthorizationCodeFlowEnabled)
            .HasDefaultValue(true);
        
        builder.Property(x => x.ImplicitFlowEnabled)
            .HasDefaultValue(false);
        
        builder.Property(x => x.DirectAccessGrantsEnabled)
            .HasDefaultValue(false);
        
        builder.Property(x => x.ClientCredentialsGrantEnabled)
            .HasDefaultValue(false);

        builder.HasOne(x => x.Realm)
            .WithMany(x => x.Clients)
            .HasForeignKey(x => x.RealmId);

        builder.HasIndex(x => x.ClientId)
            .HasDatabaseName("IX_Client_ClientId")
            .IsUnique();

        builder.HasIndex(x => new { x.RealmId, x.ClientId })
            .HasDatabaseName("IX_Client_RealmId_ClientId")
            .IsUnique();
    }
}