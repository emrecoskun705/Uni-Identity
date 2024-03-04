namespace UniIdentity.Domain.Configs;

public record ConfigId(Guid Value)
{
    public static ConfigId New() => new ConfigId(Guid.NewGuid());
}