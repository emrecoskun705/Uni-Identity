using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Domain.Clients;

public class ClientScope
{
    public ClientUniqueId ClientUniqueId { get; private set; }
    public ScopeId ScopeId { get; private set; }
    public bool DefaultScope { get; private set; }
    
    public Client Client { get; private set; }
    public Scope Scope { get; private set; }
}