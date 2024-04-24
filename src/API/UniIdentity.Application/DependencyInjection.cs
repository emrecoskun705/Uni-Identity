using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Application.Behaviours;

namespace UniIdentity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add all validators for mediatr
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        services.AddMediatR(m =>
        {
            m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                
            //Behaviour order is important here
            m.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        
        
        return services;
    }
}