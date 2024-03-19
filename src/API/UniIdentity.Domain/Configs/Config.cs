using UniIdentity.Domain.Common;
using UniIdentity.Domain.Configs.ValueObjects;
using UniIdentity.Domain.Realms;

namespace UniIdentity.Domain.Configs;

/// <summary>
/// Base class representing a configuration associated with a realm within the UniIdentity domain.
/// </summary>
public abstract class Config : BaseEntity
{
    /// <summary>
    /// Gets the unique identifier of the configuration.
    /// </summary>
    public ConfigId Id { get; }
    
    /// <summary>
    /// Gets the identifier of the realm to which the configuration belongs.
    /// </summary>
    public RealmId RealmId { get; }
    
    /// <summary>
    /// Gets or sets the name of the configuration.
    /// </summary>
    public string Name { get; private set; }
    
    /// <summary>
    /// Gets the provider type associated with the configuration.
    /// </summary>
    public ProviderType ProviderType { get; init; }
    
    /// <summary>
    /// Gets the collection of configuration attributes associated with the configuration.
    /// </summary>
    public ICollection<ConfigAttribute> ConfigAttributes { get; protected set; }
    
    /// <summary>
    /// Adds a new configuration attribute to the configuration.
    /// </summary>
    /// <param name="name">The name of the configuration attribute.</param>
    /// <param name="value">The value of the configuration attribute.</param>
    internal void AddConfigAttribute(string name, string value)
    {
        var configAttribute = new ConfigAttribute(ConfigAttributeId.New(), Id, name, value);
        ConfigAttributes.Add(configAttribute);   
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Config"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration.</param>
    /// <param name="realmId">The identifier of the realm to which the configuration belongs.</param>
    /// <param name="name">The name of the configuration.</param>
    /// <param name="providerType">The provider type associated with the configuration.</param>
    /// <param name="configAttributes">The collection of configuration attributes associated with the configuration.</param>
    protected Config(ConfigId id, RealmId realmId, string name, ProviderType providerType, ICollection<ConfigAttribute> configAttributes)
    {
        Id = id;
        RealmId = realmId;
        Name = name;
        ProviderType = providerType;
        ConfigAttributes = configAttributes;
    }
}