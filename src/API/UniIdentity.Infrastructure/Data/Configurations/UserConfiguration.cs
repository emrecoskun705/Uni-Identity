using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniIdentity.Domain.Credentials;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Users;
using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Infrastructure.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(
                x => x.Value,
                g => UserId.FromValue(g)
            );

        builder.Property(x => x.Email)
            .HasMaxLength(300)
            .HasConversion(
                c => c.Value,
                val => new Email(val)
            );
        
        builder.Property(x => x.NormalizedEmail)
            .HasMaxLength(300)
            .HasConversion(
                c => c.Value,
                val => NormalizedEmail.Create(val)
            );
        
        builder.Property(x => x.Username)
            .HasMaxLength(30)
            .HasConversion(
                c => c.Value,
                val => new Username(val)
            );
        
        builder.Property(x => x.NormalizedUsername)
            .HasMaxLength(30)
            .HasConversion(
                c => c.Value,
                val => NormalizedUsername.Create(val)
            );

        builder.Property(x => x.IdentityId)
            .HasConversion(
                c => c.Value,
                val => IdentityId.FromValue(val)
            );

        builder.HasMany<Credential>()
            .WithOne()
            .HasForeignKey(x => x.UserId);

        builder.HasOne<Realm>()
            .WithMany()
            .HasForeignKey(x => x.RealmId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(x => x.NormalizedEmail)
            .HasDatabaseName("IX_User_NormalizedEmail")
            .IsUnique();
        
        builder.HasIndex(x => x.NormalizedUsername)
            .HasDatabaseName("IX_User_NormalizedUsername")
            .IsUnique();
        
        builder.HasIndex(user => user.IdentityId)
            .HasDatabaseName("IX_User_IdentityId")
            .IsUnique();
    }
}