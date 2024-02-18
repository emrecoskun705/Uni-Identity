using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Clients;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
{
    public void Configure(EntityTypeBuilder<ClientScope> builder)
    {
        builder.HasKey(x => new { ClientId = x.ClientUniqueId, x.ScopeId });
        
        builder
            .HasOne(x => x.Client)
            .WithMany(x => x.ClientScopes)
            .HasForeignKey(x => x.ClientUniqueId);

        builder
            .HasOne(x => x.Scope)
            .WithMany(x => x.ClientScopes)
            .HasForeignKey(x => x.ScopeId);
    }
}