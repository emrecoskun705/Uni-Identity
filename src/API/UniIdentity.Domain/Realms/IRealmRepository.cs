namespace UniIdentity.Domain.Realms;

public interface IRealmRepository
{
    Task<Realm?> GetByRealmId(RealmId realmId, CancellationToken ct = default);
}