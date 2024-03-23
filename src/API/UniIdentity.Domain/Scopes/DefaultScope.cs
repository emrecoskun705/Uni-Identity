using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Scopes;

/// <summary>
/// Represents the association between a realm and a scope, indicating whether the scope is a default scope for the realm.
/// </summary>
public sealed class DefaultScope : BaseEntity
{
    /// <summary>
    /// Gets the identifier of the realm associated with the scope.
    /// </summary>
    public RealmId RealmId { get; }
    
    /// <summary>
    /// Gets the identifier of the scope associated with the realm.
    /// </summary>
    public ScopeId ScopeId { get; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the scope is a default scope for the realm.
    /// </summary>
    public bool IsDefault { get; private set; }

    internal DefaultScope(RealmId realmId, ScopeId scopeId, bool isDefault)
    {
        RealmId = realmId;
        ScopeId = scopeId;
        IsDefault = isDefault;
    }
}