namespace UniIdentity.Domain.Clients;

public record ClientId(Guid Value)
{
    public static ClientId FromValue(Guid value) => new ClientId(value);

    public static ClientId New() => FromValue(Guid.NewGuid());
}