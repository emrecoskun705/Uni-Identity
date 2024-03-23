using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Scopes;

/// <summary>
/// Represents an attribute associated with a scope, providing additional information or metadata.
/// </summary>
public class ScopeAttribute : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier of the scope attribute.
    /// </summary>
    public ScopeId Id { get; private set; }
    
    /// <summary>
    /// Gets the name of the scope attribute.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the value of the scope attribute.
    /// </summary>
    public string Value { get; private set; }
    
    public ScopeAttribute(ScopeId id, string name, string value)
    {
        Id = id;
        Name = name;
        Value = value;
    }
}