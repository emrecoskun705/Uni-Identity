using System.ComponentModel;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms.Enums;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Realms;

public sealed class Realm : BaseEntity<RealmId>
{
    public int AccessTokenLifeSpan { get; set; }
    public int SsoMaxLifeSpan { get; set; }
    public string Name { get; set; } = null!;
    public SslRequirement SslRequirement { get; set; }
    
    public bool Enabled { get; set; }
    
    public bool VerifyEmail { get; set; }
    
    public IEnumerable<User> Users { get; set; }
    public IEnumerable<Client> Clients { get; set; }
}