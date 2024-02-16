namespace UniIdentity.Domain.Clients;

/// <summary>
/// Represents a repository for managing client attributes.
/// </summary>
public interface IClientAttributeRepository
{
    /// <summary>
    /// Retrieves client attributes by client identifier asynchronously.
    /// </summary>
    /// <param name="clientId">The identifier of the client.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a collection of client attributes if found; otherwise, null.</returns>
    Task<IEnumerable<ClientAttribute>?> GetClientAttributes(ClientId clientId);
}