using UniIdentity.Domain.Clients.Enums;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles;

namespace UniIdentity.Domain.Clients;

public sealed class Client : BaseEntity<ClientId>
{
    public string ClientId { get; private set; }
    public string ClientSecret { get; private set; }
    public string Name { get; private set; }
    public Protocol? Protocol { get; private set; }
    public string BaseUrl { get; private set; }
    public string RootUrl { get; private set; }
    public string ManagementUrl { get; private set; }
    public ClientAuthenticationType ClientAuthenticationType { get; private set; }
    public string RegistrationToken { get; private set; }
    public AccessType AccessType { get; private set; }
    public RealmId RealmId { get; private set; }
    public bool PublicClient { get; private set; }
    public bool Enabled { get; private set; }
    public bool BearerOnly { get; private set; }
    public bool ConsentRequired { get; private set; }
    public bool AuthorizationCodeFlowEnabled { get; private set; }
    public bool ImplicitFlowEnabled { get; private set; }
    public bool DirectAccessGrantsEnabled { get; private set; }
    public bool ClientCredentialsGrantEnabled { get; private set; }

    public ICollection<ClientAttribute> ClientAttributes { get; set; }
    public ICollection<ClientScope> ClientScopes { get; set; }

    public Realm Realm { get; }

    private Client(
        string clientId,string clientSecret,string name,Protocol protocol,
        string baseUrl,string rootUrl,string managementUrl,ClientAuthenticationType clientAuthenticationType,
        string registrationToken,AccessType accessType,RealmId realmId,bool publicClient,bool enabled,bool bearerOnly,
        bool consentRequired,bool authorizationCodeFlowEnabled,bool implicitFlowEnabled,bool directAccessGrantsEnabled,
        bool clientCredentialsGrantEnabled)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
        Name = name;
        Protocol = protocol;
        BaseUrl = baseUrl;
        RootUrl = rootUrl;
        ManagementUrl = managementUrl;
        ClientAuthenticationType = clientAuthenticationType;
        RegistrationToken = registrationToken;
        AccessType = accessType;
        RealmId = realmId;
        PublicClient = publicClient;
        Enabled = enabled;
        BearerOnly = bearerOnly;
        ConsentRequired = consentRequired;
        AuthorizationCodeFlowEnabled = authorizationCodeFlowEnabled;
        ImplicitFlowEnabled = implicitFlowEnabled;
        DirectAccessGrantsEnabled = directAccessGrantsEnabled;
        ClientCredentialsGrantEnabled = clientCredentialsGrantEnabled;
    }
}