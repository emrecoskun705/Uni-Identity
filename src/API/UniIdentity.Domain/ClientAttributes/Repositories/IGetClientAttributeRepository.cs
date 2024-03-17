using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.ClientAttributes.Repositories;

/// <summary>
/// Represents a repository interface for retrieving client attributes asynchronously.
/// </summary>
/// <remarks>
/// Implementations of this interface provide methods to retrieve a collection of client attributes based on the specified realm identifier and client key.
/// </remarks>
/// <seealso cref="ClientAttribute"/>
public interface IGetClientAttributeRepository
{
    /// <summary>
    /// Retrieves a client attribute asynchronously by its name, realm identifier, and client key.
    /// </summary>
    /// <param name="realmId">The identifier of the realm to which the client belongs.</param>
    /// <param name="clientKey">The key associated with the client.</param>
    /// <param name="name">The name of the client attribute to retrieve.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation (optional).</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the client attribute matching the specified name, realm identifier, and client key.</returns>
    /// <remarks>
    /// Implementations of this method retrieve a specific client attribute based on its name, associated realm identifier, and client key.
    /// </remarks>
    /// <seealso cref="ClientAttribute"/>
    Task<ClientAttribute> GetByNameAsync(RealmId realmId, ClientKey clientKey, string name, CancellationToken ct = default);
}