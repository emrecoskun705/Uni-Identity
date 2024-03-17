using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
{
    public void Configure(EntityTypeBuilder<ClientScope> builder)
    {
        builder.HasKey(x => new { ClientId = x.ClientId, x.ScopeId });
        
        builder
            .HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.ClientId);

        builder
            .HasOne<Scope>()
            .WithMany()
            .HasForeignKey(x => x.ScopeId);
    }
}