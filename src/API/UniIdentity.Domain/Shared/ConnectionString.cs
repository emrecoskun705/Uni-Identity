namespace UniIdentity.Domain.Shared;

public record ConnectionString
{
    public const string DefaultDb = "Default";
    
    public string Value { get; }

    private ConnectionString(string value) => Value = value;

    public static ConnectionString DefaultDbConnectionString => new ConnectionString(DefaultDb);

    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}