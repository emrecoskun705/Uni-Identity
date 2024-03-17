namespace UniIdentity.Domain.RealmAttributes.Repositories;

public interface IAddRealmAttributeRepository
{
    Task AddAsync(RealmAttribute realm);
}