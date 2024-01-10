using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes;

public class ScopeAttribute : BaseEntity<ScopeId>
{
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public Scope Scope { get; private set; }

    public ScopeAttribute(ScopeId id, string name, string value)
        : base(id)
    {
        Name = name;
        Value = value;
    }
}