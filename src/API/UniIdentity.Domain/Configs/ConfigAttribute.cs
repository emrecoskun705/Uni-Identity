using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Configs;

public class ConfigAttribute : BaseEntity
{
    public ConfigAttributeId Id { get; private set; }
    public ConfigId ConfigId { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; }
    
    public Config Config { get; private set; }

    public ConfigAttribute(ConfigId configId, string name, string value)
    {
        ConfigId = configId;
        Name = name;
        Value = value;
    }
}