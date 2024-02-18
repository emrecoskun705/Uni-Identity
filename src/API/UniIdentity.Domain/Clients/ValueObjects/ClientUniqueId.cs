namespace UniIdentity.Domain.Clients.ValueObjects;

public record ClientUniqueId(Guid Value)
{
    public static ClientUniqueId FromValue(Guid value) => new ClientUniqueId(value);

    public static ClientUniqueId New() => FromValue(Guid.NewGuid());
}