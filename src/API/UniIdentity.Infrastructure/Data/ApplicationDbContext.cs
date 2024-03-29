﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Credentials;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Users;

namespace UniIdentity.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<User> User { get; init; } 
    public DbSet<Role> Role { get; init; } 
    public DbSet<UserRole> UserRole { get; init; } 
    public DbSet<Credential> Credential { get; init; } 
    public DbSet<Realm> Realm { get; init; } 
    public DbSet<Client> Client { get; init; }
    public DbSet<Scope> Scope { get; init; }
    public DbSet<ClientAttribute> ClientAttribute { get; init; }
    public DbSet<RealmAttribute> RealmAttribute { get; init; }
    public DbSet<Config> Config { get; init; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(builder);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }
}