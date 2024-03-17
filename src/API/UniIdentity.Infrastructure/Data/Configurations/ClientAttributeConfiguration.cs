using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.Clients;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class ClientAttributeConfiguration : IEntityTypeConfiguration<ClientAttribute>
{
    public void Configure(EntityTypeBuilder<ClientAttribute> builder)
    {
        builder.HasKey(x => new { Id = x.Id, x.Name });
        
        builder.Property(x => x.Name)
            .HasMaxLength(255);

        builder.Property(x => x.Value)
            .HasMaxLength(3000);
        
        builder.HasOne<Client>()
            .WithMany()
            .HasForeignKey(x => x.Id);
        
    }
}