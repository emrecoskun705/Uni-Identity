namespace UniIdentity.Domain.Clients;

public record ClientId(string Value)
{
    public static ClientId FromValue(string value) => new ClientId(value);
}