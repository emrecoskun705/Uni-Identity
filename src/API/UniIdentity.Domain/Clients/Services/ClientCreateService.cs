using UniIdentity.Domain.Clients.Repositories;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.ClientScopes.Repositories;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Domain.Clients.Services;

public sealed class ClientCreateService
{
    private readonly IAddClientRepository _addClientRepository;
    private readonly IAddClientScopeRepository _addClientScopeRepository;
    private readonly IGetDefaultScopeRepository _getDefaultScopeRepository;

    public ClientCreateService(
        IAddClientRepository addClientRepository, 
        IAddClientScopeRepository addClientScopeRepository, 
        IGetDefaultScopeRepository getDefaultScopeRepository)
    {
        _addClientRepository = addClientRepository;
        _addClientScopeRepository = addClientScopeRepository;
        _getDefaultScopeRepository = getDefaultScopeRepository;
    }

    public async Task AddClientWithDefaults(RealmId realmId, ClientKey clientKey, Protocol protocol, string rootUrl, CancellationToken cancellationToken = default)
    {
        var client = Client.Create(realmId, clientKey, protocol, rootUrl);
        _addClientRepository.Add(client);

        var defaultScopes = (await _getDefaultScopeRepository.GetAllByRealmId(realmId, cancellationToken: cancellationToken)).ToList();
        if (defaultScopes.Count == 0) throw new Exception("Default scope must contain some scope");

        client.AddDefaultClientScopes(_addClientScopeRepository, defaultScopes);
    }
}