using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Credentials;
using UniIdentity.Domain.Roles;
using UniIdentity.Domain.Users;

namespace UniIdentity.Infrastructure.Data;

internal sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> User { get; init; } 
    public DbSet<Role> Role { get; init; } 
    public DbSet<UserRole> UserRole { get; init; } 
    public DbSet<Credential> Credential { get; init; } 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }
}