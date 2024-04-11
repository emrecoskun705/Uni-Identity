using UniIdentity.Domain.Configs;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.Scopes;

namespace UniIdentity.Domain.Realms.Representations;

/// <summary>
/// Represents a representation of a realm including its all type of configurations.
/// </summary>
public class RealmRepresentation
{
    public required Realm Realm { get; init; }
    public required RealmAttribute AttributeSignatureAlgorithm { get; init; }
    public required IReadOnlyList<Scope> Scopes { get; init; }
    public required IReadOnlyList<DefaultScope> RealmDefaultScopes { get; init; }
    public required IReadOnlyList<RsaGenerationConfig> RsaGenerationConfigs { get; init; }
}
