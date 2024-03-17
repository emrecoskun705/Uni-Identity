using UniIdentity.Domain.Common;
using UniIdentity.Domain.RealmAttributes;
using UniIdentity.Domain.RealmAttributes.Repositories;
using UniIdentity.Domain.Realms.Consts;
using UniIdentity.Domain.Realms.Enums;

namespace UniIdentity.Domain.Realms;

public sealed class Realm : BaseEntity
{
    public RealmId Id { get; private set; }
    public int AccessTokenLifeSpan { get; private set; }
    public int SsoMaxLifeSpan { get; private set; }
    public string Name { get; private set; }
    public SslRequirement SslRequirement { get; private set; }
    public bool Enabled { get; private set; }
    public bool VerifyEmail { get; private set; }
    
    private const int DefaultLifeSpan = 60;

    private Realm(RealmId id,
        int accessTokenLifeSpan,
        int ssoMaxLifeSpan,
        string name,
        SslRequirement sslRequirement,
        bool enabled,
        bool verifyEmail)
    {
        Id = id;
        AccessTokenLifeSpan = accessTokenLifeSpan;
        SsoMaxLifeSpan = ssoMaxLifeSpan;
        Name = name;
        SslRequirement = sslRequirement;
        Enabled = enabled;
        VerifyEmail = verifyEmail;
    }

    public static Realm CreateRealmWithDefaultAttributes(
        string name,
        bool enabled
        )
    {
        var realm = new Realm(new RealmId(name), DefaultLifeSpan, DefaultLifeSpan, name, SslRequirement.None, enabled, false);
        return realm;
    }

    public async Task<string?> GetSignatureAlgorithm(IGetRealmAttributeRepository getRealmAttributeRepository, CancellationToken cancellationToken = default)
    {
        return (await getRealmAttributeRepository.GetByNameAsync(Id, RealmAttributeName.SignatureAlgorithm, cancellationToken)).Value;
    }
    
    public async Task AddAttribute(string name, string value, IAddRealmAttributeRepository addRealmAttributeRepository)
    {
        var realmAttribute = RealmAttribute.Create(Id, name, value);
        await addRealmAttributeRepository.AddAsync(realmAttribute);
    }

}