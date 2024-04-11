using UniIdentity.Domain.Configs;
using UniIdentity.Domain.Configs.Enums;
using UniIdentity.Domain.Configs.Repositories;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms.Consts;
using UniIdentity.Domain.Realms.Repositories;
using UniIdentity.Domain.Realms.Representations;
using UniIdentity.Domain.Scopes;
using UniIdentity.Domain.Scopes.Repositories;

namespace UniIdentity.Domain.Realms.Services;

/// <summary>
/// Service responsible for creating realms with default configurations.
/// </summary>
public class RealmCreateService
{
    private readonly IAddRealmRepository _addRealmRepository;
    private readonly IAddRealmAttributeRepository _addRealmAttributeRepository;
    private readonly IAddScopeRepository _addScopeRepository;
    private readonly IAddDefaultScopeRepository _addDefaultScopeRepository;
    private readonly IAddConfigRepository _addConfigRepository;

    public RealmCreateService(
        IAddRealmRepository addRealmRepository, 
        IAddRealmAttributeRepository addRealmAttributeRepository, 
        IAddScopeRepository addScopeRepository, 
        IAddDefaultScopeRepository addDefaultScopeRepository, 
        IAddConfigRepository addConfigRepository)
    {
        _addRealmRepository = addRealmRepository;
        _addRealmAttributeRepository = addRealmAttributeRepository;
        _addScopeRepository = addScopeRepository;
        _addDefaultScopeRepository = addDefaultScopeRepository;
        _addConfigRepository = addConfigRepository;
    }

    /// <summary>
    /// Adds a new realm with default configurations.
    /// </summary>
    /// <param name="name">The name of the realm.</param>
    /// <param name="enabled">Specifies whether the realm is enabled.</param>
    /// <returns>A representation of the newly created realm.</returns>
    public RealmRepresentation AddRealmWithDefaults(string name, bool enabled)
    {
        var realm = Realm.Create(name, enabled);
        
        _addRealmRepository.Add(realm);
        
        var attributeSignatureAlgorithm = AddDefaultSignatureAlgorithm(realm);

        var scopes = AddRealmScopes(realm);
     
        var defaultScopes = AddRealmDefaultScopes(realm, scopes);

        var rsaGenerationsConfig = AddRsaGenerationConfigs(realm);
        
        return new RealmRepresentation()
        {
            Realm = realm,
            Scopes = scopes,
            AttributeSignatureAlgorithm = attributeSignatureAlgorithm,
            RealmDefaultScopes = defaultScopes,
            RsaGenerationConfigs = rsaGenerationsConfig
        };
    }

    private List<RsaGenerationConfig> AddRsaGenerationConfigs(Realm realm)
    {
        var rsaGenerationConfigs = new List<RsaGenerationConfig>()
        {
            RsaGenerationConfig.CreateWithConfigurations(realm.Id, "default-rsa")
        };
        
        rsaGenerationConfigs.ForEach(
            x => _addConfigRepository.Add(x));

        return rsaGenerationConfigs;
    }
    
    private RealmAttribute AddDefaultSignatureAlgorithm(Realm realm)
    {
        var defaultSignatureAlgorithm =
            realm.CreateAttribute(RealmAttributeName.SignatureAlgorithm, SignatureAlg.Default);
        
        _addRealmAttributeRepository.Add(defaultSignatureAlgorithm);
        return defaultSignatureAlgorithm;
    }
    
    private IReadOnlyList<Scope> AddRealmScopes(Realm realm)
    {
        return realm.AddScopes(_addScopeRepository);
    }
    
    private IReadOnlyList<DefaultScope> AddRealmDefaultScopes(Realm realm, IReadOnlyList<Scope> scopes)
    {
        return realm.AddDefaultScopes(_addDefaultScopeRepository, scopes);
    }
}