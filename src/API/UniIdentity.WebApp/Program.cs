using UniIdentity.WebApp.Extensions;

var builder = WebApplication
    .CreateBuilder(args)
    .ConfigureApplicationBuilder();

var app = await builder.Build()
        .ConfigureApplication();

app.Run();