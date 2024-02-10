using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Application.Contracts.Data;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Users;
using UniIdentity.Infrastructure.Data;
using UniIdentity.Infrastructure.Data.Interceptors;
using UniIdentity.Infrastructure.Data.Repositories;

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

        services.AddScoped<ApplicationDbContextInitializer>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRealmRepository, RealmRepository>();
        
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());
        

        return services;
    }
}