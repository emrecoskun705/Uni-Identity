using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients.Enums;
using UniIdentity.Domain.Clients.Events;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

public sealed class Client : AggregateRoot
{
    public ClientId Id { get; }
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

    public async Task<string?> GetAttribute(string attribute, IGetClientAttributeRepository getClientAttributeRepository)
    {
        return (await getClientAttributeRepository.GetByNameAsync(RealmId, ClientKey, attribute)).Value;
    }
    
    public static Client Create(RealmId realmId, ClientKey clientKey, Protocol protocol, string rootUrl)
    {
        var client = new Client
        {
            ClientKey = clientKey,
            RealmId = realmId,
            RootUrl = rootUrl,
            ManagementUrl = rootUrl,
            Protocol = protocol,
            Enabled = true,
            AuthorizationCodeFlowEnabled = true,
            ClientAuthenticationType = ClientAuthenticationType.ClientSecret,
            DirectAccessGrantsEnabled = true,
            AccessType = AccessType.Public
        };
        client.AddDomainEvent(new ClientCreatedEvent(client.Id));
        return client;
    }
    
    private Client(ClientKey clientKey,string? clientSecret,string? name,Protocol? protocol,
        string? baseUrl, string? rootUrl ,string? managementUrl, ClientAuthenticationType clientAuthenticationType,
        string? registrationToken,AccessType accessType,RealmId realmId,bool enabled,
        bool consentRequired,bool authorizationCodeFlowEnabled,bool implicitFlowEnabled,bool directAccessGrantsEnabled,
        bool clientCredentialsGrantEnabled)
    {
        Id = ClientId.New();
        ClientKey = clientKey;
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
        Enabled = enabled;
        ConsentRequired = consentRequired;
        AuthorizationCodeFlowEnabled = authorizationCodeFlowEnabled;
        ImplicitFlowEnabled = implicitFlowEnabled;
        DirectAccessGrantsEnabled = directAccessGrantsEnabled;
        ClientCredentialsGrantEnabled = clientCredentialsGrantEnabled;
    }

    private Client()
    {
        Id = ClientId.New();
    }
}
