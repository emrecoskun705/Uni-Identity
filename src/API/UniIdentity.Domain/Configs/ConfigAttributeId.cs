namespace UniIdentity.Domain.Configs;

public record ConfigAttributeId(Guid Value)
{
    public static ConfigAttributeId New() => new ConfigAttributeId(Guid.NewGuid());
};