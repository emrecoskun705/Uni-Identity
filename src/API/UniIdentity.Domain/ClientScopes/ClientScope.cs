using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Domain.ClientScopes;

public class ClientScope : BaseEntity
{
    public ClientId ClientId { get; private set; }
    public ScopeId ScopeId { get; private set; }
    public bool DefaultScope { get; private set; }

    public ClientScope(ClientId clientId, ScopeId scopeId, bool defaultScope)
    {
        ClientId = clientId;
        ScopeId = scopeId;
        DefaultScope = defaultScope;
    }
}