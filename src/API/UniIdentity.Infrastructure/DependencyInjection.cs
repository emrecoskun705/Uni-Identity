﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Application.Contracts.Data;
using UniIdentity.Application.Contracts.Sessions;
using UniIdentity.Domain;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.ClientScopes.Repositories;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs.Repositories;
using UniIdentity.Domain.Credentials.Services;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms.Repositories;
using UniIdentity.Domain.Scopes.Repositories;
using UniIdentity.Domain.Users;
using UniIdentity.Infrastructure.Context;
using UniIdentity.Infrastructure.Cryptography;
using UniIdentity.Infrastructure.Data;
using UniIdentity.Infrastructure.Data.Interceptors;
using UniIdentity.Infrastructure.Data.Repositories;
using UniIdentity.Infrastructure.Sessions;

namespace UniIdentity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? throw new ArgumentNullException("connectionString");
        
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            
            options.UseNpgsql(connectionString);

            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        #region Dapper
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion

        services.AddHttpContextAccessor();
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<IUserRepository, UserRepository>();
        
        services.AddKeyedScoped<IGetRealmRepository, RealmRepository>(ServiceKey.RealmOriginalKey);
        services.AddScoped<IAddRealmRepository, RealmRepository>();
        services.AddScoped<IGetRealmRepository, CachedRealmRepository>();
        
        services.AddKeyedScoped<IGetRealmAttributeRepository, RealmAttributeRepository>(ServiceKey.RealmOriginalKey);
        services.AddScoped<IAddRealmAttributeRepository, RealmAttributeRepository>();
        services.AddScoped<IGetRealmAttributeRepository, CachedRealmAttributeRepository>();
        
        services.AddKeyedScoped<IGetClientRepository, ClientRepository>(ServiceKey.ClientOriginalKey);
        services.AddScoped<IAddClientRepository, ClientRepository>();
        services.AddScoped<IGetClientRepository, CachedClientRepository>();
        services.AddScoped<IClientExistenceRepository, ClientRepository>();
        
        services.AddKeyedScoped<IGetClientAttributeRepository, ClientAttributeRepository>(ServiceKey.ClientOriginalKey);
        services.AddScoped<IAddClientAttributeRepository, ClientAttributeRepository>();
        services.AddScoped<IGetClientAttributeRepository, CachedClientAttributeRepository>();

        services.AddKeyedScoped<IGetClientScopeRepository, ClientScopeRepository>(ServiceKey.ClientScopeOriginalKey);
        services.AddScoped<IAddClientScopeRepository, ClientScopeRepository>();
        services.AddScoped<IGetClientScopeRepository, CachedClientScopeRepository>();

        services.AddKeyedScoped<IGetScopeRepository, ScopeRepository>(ServiceKey.ScopeOriginalKey);
        services.AddScoped<IAddScopeRepository, ScopeRepository>();
        services.AddScoped<IGetScopeRepository, ScopeRepository>();

        services.AddScoped<IAddDefaultScopeRepository, DefaultScopeRepository>();
        
        services.AddKeyedScoped<IGetConfigRepository, ConfigRepository>(ServiceKey.ConfigOriginalKey);
        services.AddScoped<IGetConfigRepository, CachedConfigRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton<ISessionManager, SessionManager>();
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IUniHttpContext, UniHttpContext>();

        services.AddDistributedMemoryCache();

        #region Cryprography

        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IPasswordVerifier, PasswordHasher>();

        #endregion

        return services;
    }
}