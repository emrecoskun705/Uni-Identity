using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using Asp.Versioning.Builder;
using UniIdentity.Infrastructure.Data;

namespace UniIdentity.WebApp.Extensions;

[ExcludeFromCodeCoverage]
internal static class WebApplicationExtensions
{
    public static async Task<WebApplication> ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            await app.InitializeDatabaseAsync();
        }

        ApiVersionSet apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();

        RouteGroupBuilder versionedGroup = app
            .MapGroup("api/v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
        
        app.MapEndpoints(versionedGroup);
        
        return app;
    }
}