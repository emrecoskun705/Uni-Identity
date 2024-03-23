using UniIdentity.Domain.ClientAttributes;
using UniIdentity.Domain.ClientAttributes.Consts;
using UniIdentity.Domain.ClientAttributes.Repositories;
using UniIdentity.Domain.Clients.Enums;
using UniIdentity.Domain.Clients.Events;
using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Clients;

/// <summary>
/// Represents a client entity within the UniIdentity domain, capable of requesting user authentication.
/// </summary>
/// <remarks>
/// Clients are entities, typically applications and services, that utilize the identity system for authentication purposes. They may also request identity information or access tokens to securely invoke other services within the network.
/// </remarks>
public sealed class Client : AggregateRoot
{
    /// <summary>
    /// Gets the unique identifier of the client.
    /// </summary>
    public ClientId Id { get; }
    
    /// <summary>
    /// Gets or sets the unique client key within the realm.
    /// </summary>
    public ClientKey ClientKey { get; private set; }
    
    /// <summary>
    /// Gets or sets the client secret used for authentication (if applicable).
    /// </summary>
    public string? ClientSecret { get; private set; }
    
    /// <summary>
    /// Gets or sets the name of the client.
    /// </summary>
    public string? Name { get; private set; }
    
    /// <summary>
    /// Gets or sets the protocol used by the client.
    /// </summary>
    public Protocol? Protocol { get; private set; }
    
    /// <summary>
    /// Gets or sets the base URL of the client.
    /// </summary>
    public string? BaseUrl { get; private set; }
    
    /// <summary>
    /// Gets or sets the root URL of the client.
    /// </summary>
    public string? RootUrl { get; private set; }
    
    /// <summary>
    /// Gets or sets the management URL of the client.
    /// </summary>
    public string? ManagementUrl { get; private set; }
    
    /// <summary>
    /// Gets or sets the authentication type used by the client.
    /// </summary>
    public ClientAuthenticationType ClientAuthenticationType { get; private set; }
    
    /// <summary>
    /// Gets or sets the registration token of the client.
    /// </summary>
    public string? RegistrationToken { get; private set; }
    
    /// <summary>
    /// Gets or sets the access type of the client.
    /// </summary>
    public AccessType AccessType { get; private set; }
    
    /// <summary>
    /// Gets or sets the realm identifier to which the client belongs.
    /// </summary>
    public RealmId RealmId { get; private set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether the client is enabled.
    /// </summary>
    public bool Enabled { get; private set; }
    
    /// <summary>
    /// Gets or sets a value indicating whether consent is required for the client.
    /// </summary>
    public bool ConsentRequired { get; private set; }
    /// <summary>
    /// Gets or sets a value indicating whether the authorization code flow is enabled for the client.
    /// </summary>
    public bool AuthorizationCodeFlowEnabled { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the implicit flow is enabled for the client.
    /// </summary>
    public bool ImplicitFlowEnabled { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether direct access grants are enabled for the client.
    /// </summary>
    public bool DirectAccessGrantsEnabled { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether client credentials grant is enabled for the client.
    /// </summary>
    public bool ClientCredentialsGrantEnabled { get; private set; }

    /// <summary>
    /// Retrieves the value of the specified attribute associated with the client.
    /// </summary>
    /// <param name="attribute">The name of the attribute to retrieve.</param>
    /// <param name="getClientAttributeRepository">The repository for retrieving client attributes.</param>
    /// <returns>The value of the attribute associated with the client, or <c>null</c> if not found.</returns>
    /// <remarks>
    /// This method asynchronously retrieves the value of the specified attribute associated with the client.
    /// </remarks>
    public async Task<string?> GetAttribute(string attribute, IGetClientAttributeRepository getClientAttributeRepository)
    {
        return (await getClientAttributeRepository.GetByNameAsync(RealmId, ClientKey, attribute)).Value;
    }

    /// <summary>
    /// Creates a new client attribute with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The value of the attribute.</param>
    /// <returns>A new <see cref="ClientAttribute"/> instance with the specified name and value.</returns>
    /// <remarks>
    /// This method creates a new client attribute with the specified name and value.
    /// </remarks>
    public ClientAttribute CreateAttribute(string name, string value)
    {
        var clientAttribute = new ClientAttribute(Id, name, value);
        return clientAttribute;
    }

    public IEnumerable<ClientAttribute> AddDefaultClientAttributes(IAddClientAttributeRepository addClientAttributeRepository)
    {
        var enableRefreshTokenAttribute = CreateAttribute(
            ClientAttributeName.EnableRefreshToken, 
            true.ToString()); 
        
        addClientAttributeRepository.Add(enableRefreshTokenAttribute);

        return new[]
        {
            enableRefreshTokenAttribute
        };
    }
    
    /// <summary>
    /// Creates a new client with the specified parameters.
    /// </summary>
    /// <param name="realmId">The realm identifier to which the client belongs.</param>
    /// <param name="clientKey">The unique client key within the realm.</param>
    /// <param name="protocol">The protocol used by the client.</param>
    /// <param name="rootUrl">The root URL of the client.</param>
    /// <returns>A new <see cref="Client"/> instance with the specified parameters.</returns>
    /// <remarks>
    /// This method creates a new client entity with the specified parameters.
    /// </remarks>
    public static Client Create(RealmId realmId, ClientKey clientKey, Protocol protocol, string rootUrl)
    {
        var client = new Client(clientKey, ClientAuthenticationType.ClientSecret, realmId)
        {
            ClientKey = clientKey,
            RootUrl = rootUrl,
            ManagementUrl = rootUrl,
            Protocol = protocol,
            Enabled = true,
            AuthorizationCodeFlowEnabled = true,
            DirectAccessGrantsEnabled = true,
            AccessType = AccessType.Public
        };
        client.AddDomainEvent(new ClientCreatedEvent(client.Id));
        return client;
    }
    
    // Private constructors to enforce creation through factory methods
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

    // Private constructors to enforce creation through factory methods
    private Client(ClientKey clientKey, ClientAuthenticationType clientAuthenticationType, RealmId realmId)
    {
        Id = ClientId.New();
        ClientKey = clientKey;
        ClientAuthenticationType = clientAuthenticationType;
        RealmId = realmId;
    }
}
