using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Configs;

public class ConfigAttribute : BaseEntity
{
    public ConfigAttributeId Id { get; }
    public ConfigId ConfigId { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public Config? Config { get;  }

    internal ConfigAttribute(ConfigAttributeId id, ConfigId configId, string name, string value)
    {
        Id = id;
        ConfigId = configId;
        Name = name;
        Value = value;
    }
}