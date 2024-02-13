namespace UniIdentity.Domain.Clients;

public interface IClientAttributeRepository
{
    Task<IEnumerable<ClientAttribute>?> GetClientAttributes(ClientId clientId);
}