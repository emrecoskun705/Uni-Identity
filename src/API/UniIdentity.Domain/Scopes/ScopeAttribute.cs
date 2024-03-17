using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Scopes;

public class ScopeAttribute : BaseEntity
{
    public ScopeId Id { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public ScopeAttribute(ScopeId id, string name, string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}