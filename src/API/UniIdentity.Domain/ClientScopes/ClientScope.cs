using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Domain.ClientScopes;

/// <summary>
/// Represents the association between a client and a scope, indicating whether the scope is a default scope for the client.
/// </summary>
public class ClientScope : BaseEntity
{
    /// <summary>
    /// Gets the identifier of the client associated with the scope.
    /// </summary>
    public ClientId ClientId { get; private set; }
    
    /// <summary>
    /// Gets the identifier of the scope associated with the client.
    /// </summary>
    public ScopeId ScopeId { get; private set; }
    
    /// <summary>
    /// Gets a value indicating whether the scope is a default scope for the client.
    /// </summary>
    public bool DefaultScope { get; private set; }

    public ClientScope(ClientId clientId, ScopeId scopeId, bool defaultScope)
    {
        ClientId = clientId;
        ScopeId = scopeId;
        DefaultScope = defaultScope;
    }
}