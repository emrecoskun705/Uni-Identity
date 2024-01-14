using System.Diagnostics.CodeAnalysis;
using UniIdentity.Infrastructure.Data;

namespace UniIdentity.WebApp.Extensions;

[ExcludeFromCodeCoverage]
internal static class WebApplicationExtensions
{
    public static async Task<WebApplication> ConfigureApplication(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            await app.InitializeDatabaseAsync();
        }
        
        return app;
    }
}