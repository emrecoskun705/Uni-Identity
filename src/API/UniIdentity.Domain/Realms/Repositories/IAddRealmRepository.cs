namespace UniIdentity.Domain.Realms.Repositories;

public interface IAddRealmRepository
{
    Task AddAsync(Realm realm);
}