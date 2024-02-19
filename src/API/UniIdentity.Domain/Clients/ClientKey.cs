namespace UniIdentity.Domain.Clients;

public record ClientKey(string Value)
{
    public static ClientKey FromValue(string value) => new ClientKey(value);
}