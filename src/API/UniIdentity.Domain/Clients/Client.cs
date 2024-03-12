using UniIdentity.Domain.Clients.Enums;
using UniIdentity.Domain.Clients.Events;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.OIDC;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Roles;

namespace UniIdentity.Domain.Clients;

public sealed class Client : BaseEntity
{
    public ClientId Id { get; private set; }
    public ClientKey ClientKey { get; private set; }
    public string? ClientSecret { get; private set; }
    public string? Name { get; private set; }
    public Protocol? Protocol { get; private set; }
    public string? BaseUrl { get; private set; }
    public string? RootUrl { get; private set; }
    public string? ManagementUrl { get; private set; }
    public ClientAuthenticationType ClientAuthenticationType { get; private set; }
    public string? RegistrationToken { get; private set; }
    public AccessType AccessType { get; private set; }
    public RealmId RealmId { get; private set; }
    public bool Enabled { get; private set; }
    public bool ConsentRequired { get; private set; }
    public bool AuthorizationCodeFlowEnabled { get; private set; }
    public bool ImplicitFlowEnabled { get; private set; }
    public bool DirectAccessGrantsEnabled { get; private set; }
    public bool ClientCredentialsGrantEnabled { get; private set; }

    public ICollection<ClientAttribute> ClientAttributes { get; set; }
    public ICollection<ClientScope> ClientScopes { get; set; }
    public ICollection<Role> Roles { get; set; }

    public Realm Realm { get; }

    public string? GetSignatureAlgorithm()
    {
        return ClientAttributes.FirstOrDefault(x => x.Name == OIDCAttribute.AccessTokenAlgorithm)?.Value;
    }

    public static Client Create(ClientTemplate clientTemplate)
    {
        
        var client = new Client(clientTemplate);
        client.AddDomainEvent(new ClientCreatedEvent(client.Id));
        return client;
    }
    
    private Client(ClientTemplate clientTemplate)
    {
        Id = ClientId.New();
        ClientKey = clientTemplate.ClientKey;
        ClientSecret = clientTemplate.ClientSecret;
        Name = clientTemplate.Name;
        Protocol = clientTemplate.Protocol;
        BaseUrl = clientTemplate.BaseUrl;
        RootUrl = clientTemplate.RootUrl;
        ManagementUrl = clientTemplate.ManagementUrl;
        ClientAuthenticationType = clientTemplate.ClientAuthenticationType;
        RegistrationToken = clientTemplate.RegistrationToken;
        AccessType = clientTemplate.AccessType;
        RealmId = clientTemplate.RealmId;
        Enabled = clientTemplate.Enabled;
        ConsentRequired = clientTemplate.ConsentRequired;
        AuthorizationCodeFlowEnabled = clientTemplate.AuthorizationCodeFlowEnabled;
        ImplicitFlowEnabled = clientTemplate.ImplicitFlowEnabled;
        DirectAccessGrantsEnabled = clientTemplate.DirectAccessGrantsEnabled;
        ClientCredentialsGrantEnabled = clientTemplate.ClientCredentialsGrantEnabled;
    }

    private Client()
    {
        
    }
}

public record ClientTemplate(ClientKey ClientKey,string? ClientSecret,string? Name,Protocol? Protocol,
    string? BaseUrl, string? RootUrl ,string? ManagementUrl, ClientAuthenticationType ClientAuthenticationType,
    string? RegistrationToken,AccessType AccessType,RealmId RealmId,bool Enabled,
    bool ConsentRequired,bool AuthorizationCodeFlowEnabled,bool ImplicitFlowEnabled,bool DirectAccessGrantsEnabled,
    bool ClientCredentialsGrantEnabled);