using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

public interface IClientRepository
{
    Task<Client?> GetByClientIdAndRealmId(string clientId, RealmId realmId, CancellationToken ct = default);
}