using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Shared;

namespace UniIdentity.Application.Clients.Commands.AddClientRequest;

public sealed record AddClientRequestCommand : ICommand<Result>
{
    public RealmId RealmId { get; }
    public ClientKey ClientKey { get; }
    public Protocol Protocol { get; }
    public string RootUrl { get; }

    public AddClientRequestCommand(RealmId realmId, ClientKey clientKey, Protocol protocol, string rootUrl)
    {
        RealmId = realmId;
        ClientKey = clientKey;
        Protocol = protocol;
        RootUrl = rootUrl;
    }
}