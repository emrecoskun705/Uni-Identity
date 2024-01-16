using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms.Enums;
using UniIdentity.Domain.Roles;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Realms;

public sealed class Realm : BaseEntity<RealmId>
{
    public int AccessTokenLifeSpan { get; private set; }
    public int SsoMaxLifeSpan { get; private set; }
    public string Name { get; private set; }
    public SslRequirement SslRequirement { get; private set; }
    
    public bool Enabled { get; private set; }
    
    public bool VerifyEmail { get; private set; }
    
    public ICollection<User> Users { get; private set; }
    public ICollection<Client> Clients { get; private set; }
    public ICollection<RealmAttribute> RealmAttributes { get; private set; }

    public Realm(RealmId id,
        int accessTokenLifeSpan,
        int ssoMaxLifeSpan,
        string name,
        SslRequirement sslRequirement,
        bool enabled,
        bool verifyEmail)
        : base(id)
    {
        AccessTokenLifeSpan = accessTokenLifeSpan;
        SsoMaxLifeSpan = ssoMaxLifeSpan;
        Name = name;
        SslRequirement = sslRequirement;
        Enabled = enabled;
        VerifyEmail = verifyEmail;
    }
}