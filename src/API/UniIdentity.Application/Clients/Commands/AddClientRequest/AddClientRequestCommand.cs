using UniIdentity.Application.Contracts.Messaging;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Application.Clients.Commands.AddClientRequest;

public sealed record AddClientRequestCommand : ICommand
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