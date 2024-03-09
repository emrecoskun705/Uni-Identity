using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs.Enums;
using UniIdentity.Domain.Realms.Consts;
using UniIdentity.Domain.Realms.Enums;
using UniIdentity.Domain.Roles;
using UniIdentity.Domain.Users;

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
    
    public ICollection<User> Users { get; private set; }
    public ICollection<Client> Clients { get; private set; }
    public ICollection<RealmAttribute> RealmAttributes { get; private set; }
    public ICollection<Role> Roles { get; private set; }
    
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
        var realm = new Realm(new RealmId(name), DefaultLifeSpan, DefaultLifeSpan, name, SslRequirement.None, enabled, false)
        {
            RealmAttributes = new List<RealmAttribute>()
        };
        
        var defaultSignatureAlgorithm = SignatureAlg.RsaSha256;
        realm.AddAttribute(RealmAttributeName.SignatureAlgorithm, defaultSignatureAlgorithm);
        return realm;
    }

    public string GetSignatureAlgorithm()
    {
        var signatureAlgorithm = RealmAttributes.First(x => x.Name == RealmAttributeName.SignatureAlgorithm).Value;

        return signatureAlgorithm;
    }
    
    public void AddAttribute(string name, string value)
    {
        if (RealmAttributes == null)
            RealmAttributes = new List<RealmAttribute>();
        
        RealmAttributes.Add(RealmAttribute.Create(name, value));
    }

}