using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes.Repositories;

public interface IGetScopeRepository
{
    Task<IReadOnlyCollection<Scope>> GetAllAsync(RealmId realmId, CancellationToken ct = default);
}