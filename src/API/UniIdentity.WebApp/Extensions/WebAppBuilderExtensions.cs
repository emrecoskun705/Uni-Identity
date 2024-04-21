using System.Diagnostics.CodeAnalysis;
using Asp.Versioning;
using UniIdentity.Application;
using UniIdentity.Infrastructure;

namespace UniIdentity.WebApp.Extensions;

[ExcludeFromCodeCoverage]
internal static class WebAppBuilderExtensions
{
    public static WebApplicationBuilder ConfigureApplicationBuilder(this WebApplicationBuilder builder)
    {
        #region Project Dependencies

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });
        
        builder.Services.AddEndpoints(typeof(Program).Assembly);
        
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddMemoryCache();
        
        #endregion Project Dependencies
        
        return builder;
    }
}