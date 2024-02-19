using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Application.Contracts.Context;
using UniIdentity.Application.Contracts.Data;
using UniIdentity.Application.Contracts.Sessions;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Users;
using UniIdentity.Infrastructure.Context;
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
        });

        #region Dapper
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
        #endregion

        services.AddHttpContextAccessor();
        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddKeyedScoped<IRealmRepository, RealmRepository>("og");
        services.AddScoped<IRealmRepository, CachedRealmRepository>();
        services.AddKeyedScoped<IClientRepository, ClientRepository>("og");
        services.AddScoped<IClientRepository, CachedClientRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddSingleton<ISessionManager, SessionManager>();
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<IUniHttpContextAccessor, UniHttpContextAccessor>();

        services.AddDistributedMemoryCache();

        return services;
    }
}