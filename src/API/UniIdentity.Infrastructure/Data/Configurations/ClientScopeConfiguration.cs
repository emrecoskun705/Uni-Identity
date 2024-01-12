using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Clients;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
{
    public void Configure(EntityTypeBuilder<ClientScope> builder)
    {
        builder.HasKey(x => new { x.ClientId, x.ScopeId });
        
        builder
            .HasOne(x => x.Client)
            .WithMany(x => x.ClientScopes)
            .HasForeignKey(x => x.ClientId);

        builder
            .HasOne(x => x.Scope)
            .WithMany(x => x.ClientScopes)
            .HasForeignKey(x => x.ScopeId);
    }
}