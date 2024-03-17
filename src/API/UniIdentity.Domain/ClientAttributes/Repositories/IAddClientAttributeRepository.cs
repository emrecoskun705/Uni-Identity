namespace UniIdentity.Domain.ClientAttributes.Repositories;

public interface IAddClientAttributeRepository
{
    Task AddAsync(ClientAttribute clientAttribute);
}