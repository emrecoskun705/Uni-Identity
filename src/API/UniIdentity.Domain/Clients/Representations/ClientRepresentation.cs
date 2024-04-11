using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Domain.Clients.Representations;

/// <summary>
/// Represents a representation of a client including its all type of configurations.
/// </summary>
public class ClientRepresentation
{
    public required Client Client { get; init; }
    public required List<DefaultScope> DefaultScopes { get; init; }
    public required List<ClientAttribute> ClientAttributes { get; init; }
}