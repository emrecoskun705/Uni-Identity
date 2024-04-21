using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniIdentity.Application.Realms.Commands.AddRealmRequest;
using UniIdentity.WebApp.Abstractions;
using UniIdentity.WebApp.Extensions;

namespace UniIdentity.WebApp.Endpoints.Realms;

public class CreateRealm : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        _ = app.MapPost(ApiRoutes.Realms.Create, RequestHandler)
            .WithTags(ApiTags.Realm)
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }
    
    private static async Task<IResult> RequestHandler([FromBody] CreateRealmRequest request, IMediator mediator)
        => (await mediator.Send(request.ToCommand()))
            .ToCreated();
}

public struct CreateRealmRequest
{
    public string Name { get; set; }
    public bool Enabled { get; set; }

    [JsonConstructor]
    public CreateRealmRequest(string name, bool enabled)
    {
        Name = name;
        Enabled = enabled;
    }

    public AddRealmRequestCommand ToCommand()
    {
        return new AddRealmRequestCommand(
            Name,
            Enabled
        );
    }
}