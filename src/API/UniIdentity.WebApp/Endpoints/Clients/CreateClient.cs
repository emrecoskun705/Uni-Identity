using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UniIdentity.Application.Clients.Commands.AddClientRequest;
using UniIdentity.Domain.Realms;
using UniIdentity.WebApp.Abstractions;
using UniIdentity.WebApp.Extensions;

namespace UniIdentity.WebApp.Endpoints.Clients;

public class CreateClient : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        _ = app.MapPost(ApiRoutes.Clients.Create, RequestHandler)
            .WithTags(ApiTags.Client)
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();
    }

    private static async Task<IResult> RequestHandler([FromBody] CreateClientRequest request, IMediator mediator)
        => (await mediator.Send(request.ToCommand()))
            .ToCreated();
}

public struct CreateClientRequest
{
    public string RealmId { get; set; }
    public string ClientKey { get; set; }
    public string Protocol { get; set; }
    public string RootUrl { get; set; }

    [JsonConstructor]
    public CreateClientRequest(string realmId, string clientKey, string protocol, string rootUrl)
    {
        RealmId = realmId;
        ClientKey = clientKey;
        Protocol = protocol;
        RootUrl = rootUrl;
    }

    public AddClientRequestCommand ToCommand()
    {
        return new AddClientRequestCommand(
            new RealmId(RealmId),
            Domain.Clients.ClientKey.FromValue(ClientKey),
            Domain.Clients.ValueObjects.Protocol.FromValue(Protocol),
            RootUrl
        );
    }
}