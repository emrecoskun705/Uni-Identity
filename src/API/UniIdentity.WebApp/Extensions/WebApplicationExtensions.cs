using System.Diagnostics.CodeAnalysis;

namespace UniIdentity.WebApp.Extensions;

[ExcludeFromCodeCoverage]
internal static class WebApplicationExtensions
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        return app;
    }
}