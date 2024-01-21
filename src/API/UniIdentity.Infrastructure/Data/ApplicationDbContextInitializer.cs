﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.Enums;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Realms.Enums;
using UniIdentity.Domain.Roles;
using UniIdentity.Domain.Roles.ValueObjects;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Infrastructure.Data;

public static class InitializerExtensions 
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        await initializer.InitializeAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer
{
    private readonly ILogger<ApplicationDbContextInitializer> _logger;
    private readonly ApplicationDbContext _context;

    private const string MasterRealmId = "master";

    
    public ApplicationDbContextInitializer(
        ILogger<ApplicationDbContextInitializer> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitializeAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while initialising the database.");
            throw;
        }
    }
    
    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        // order important
        
        await AddMasterRealm();
        
        await AddDefaultScopesToMaster();

        await AddDefaultRolesToMaster();

        await AddDefaultClientsToMaster();
    }

    private async Task AddDefaultClientsToMaster()
    {
        var defaultClients = new List<Client>
        {
            Client.Create(new ClientTemplate("account", null, "account", Protocol.OpenIdConnect, "/realms/master/account/", null, null, ClientAuthenticationType.ClientSecret, null, AccessType.Public, new RealmId(MasterRealmId), true, false, true, false, false, false)),
            Client.Create(new ClientTemplate("account-console", null, "account-console", Protocol.OpenIdConnect, "/realms/master/account/", null, null, ClientAuthenticationType.ClientSecret, null, AccessType.Public, new RealmId(MasterRealmId), true, false, true, false, false, false)),
            Client.Create(new ClientTemplate("admin-console", null, "admin-console", Protocol.OpenIdConnect, "/admin/master/console/", null, null, ClientAuthenticationType.ClientSecret, null, AccessType.Public, new RealmId(MasterRealmId), true, false, true, false, false, false))
        };

        foreach (var defaultClient in defaultClients)
        {
            var client = await _context.Client.FirstOrDefaultAsync(x => x.ClientId == defaultClient.ClientId && x.RealmId == defaultClient.RealmId);

            if (client == null)
            {
                _context.Add(defaultClient);
            }
        }

        await _context.SaveChangesAsync();
    }
    
    private async Task AddDefaultRolesToMaster()
    {
        
        var masterRealmRoles = new List<RealmRoleDto>()
        {
            new("81596A81-52E0-4A97-85B8-BAFA2A38CEE9", "default-roles-master"),
            new("60E092CC-E651-4489-B945-AF763EB5C306", "admin"), 
            new("A4D5165B-D733-4A88-AA1C-D1C31539EF1E", "create-realm") 
        };

        foreach (var realmRoleDto in masterRealmRoles)
        {
            var realmRole = await _context.Role.AsNoTracking().FirstOrDefaultAsync(
                x => x.Id == RoleId.FromValue(Guid.Parse(realmRoleDto.Id)));

            if (realmRole == null)
            {
                realmRole = Role.CreateRealmRole(
                    RoleId.FromValue(Guid.Parse(realmRoleDto.Id)),
                    new Name(realmRoleDto.Name),
                    new RealmId(MasterRealmId));
            
                _context.Add(realmRole);
            }
        }


        await _context.SaveChangesAsync();
    }
    
    private async Task AddDefaultScopesToMaster()
    {
        var defaultScopes = new List<ScopeDto>()
        {
            new ScopeDto("0E3F4F4C-8C06-401D-9B8A-B8142A484117", "offline_access",
                "OpenID Connect built-in scope: offline_access"),
            new ScopeDto("BE5F6D07-82FB-4032-9928-A2164FAD44C6", "profile", "OpenID Connect built-in scope: profile"),
            new ScopeDto("1EB50C36-DBA9-4C15-98B3-2FB5CD8F579F", "email", "OpenID Connect built-in scope: email"),
            new ScopeDto("28B82C24-CF99-43AF-ADFF-4A34C6D31E0A", "address", "OpenID Connect built-in scope: address"),
            new ScopeDto("E3C5208E-406B-420F-B400-231D4BEBF27E", "phone", "OpenID Connect built-in scope: phone"),
            new ScopeDto("CD9682B2-7725-4D92-BB06-DAF390BD05A6", "roles",
                "OpenID Connect scope for add user roles to the access token"),
            new ScopeDto("E633F3C0-5065-4DF5-816A-1A1C92D0CDB6", "web-origins",
                "OpenID Connect scope for add allowed web origins to the access token"),
            new ScopeDto("1063B956-5216-493F-BE80-4ED71B09F2E7", "microprofile-jwt",
                "Microprofile - JWT built-in scope"),
        };
        
        foreach (var scope in defaultScopes)
        {
            var addScope =
                await _context.Scope.FirstOrDefaultAsync(x => x.Id == ScopeId.FromValue( Guid.Parse(scope.Id)));

            if (addScope == null)
            {
                addScope = new Scope(
                    ScopeId.FromValue(Guid.Parse(scope.Id)),
                    scope.Name,
                    "openid-connect",
                    new RealmId("master"),
                    scope.Description
                );

                _context.Add(addScope);
            }
        }

        await _context.SaveChangesAsync();
    }
    
    private async Task AddMasterRealm()
    {
        var masterRealm = await _context.Realm.FirstOrDefaultAsync(x => x.Id == new RealmId("master"));

        if (masterRealm == null)
        {
            masterRealm = new Realm(
                new RealmId("master"),
                300,
                300,
                "master",
                SslRequirement.None,
                true,
                false
            );

            _context.Add<Realm>(masterRealm);
            
            await _context.SaveChangesAsync();
        }
    }

    private class ScopeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ScopeDto(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }

    private class RealmRoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public RealmRoleDto(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    
}