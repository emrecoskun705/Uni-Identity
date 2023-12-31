﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using UniIdentity.Application.Behaviours;

namespace UniIdentity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(m =>
        {
            m.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            
            //Behaviour order is important here
            m.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        // Add all validators for mediatr
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}