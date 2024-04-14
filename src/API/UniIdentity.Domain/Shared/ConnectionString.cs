namespace UniIdentity.Domain.Shared;

/// <summary>
/// Represents a connection string for a database.
/// </summary>
public record ConnectionString
{
    /// <summary>
    /// The default database connection string name.
    /// </summary>
    public const string DefaultDb = "Default";
    
    /// <summary>
    /// Gets the value of the connection string.
    /// </summary>
    public string Value { get; }

    private ConnectionString(string value) => Value = value;

    /// <summary>
    /// Gets the default database connection string.
    /// </summary>
    public static ConnectionString DefaultDbConnectionString => new ConnectionString(DefaultDb);

    /// <summary>
    /// Implicitly converts a <see cref="ConnectionString"/> object to a <see cref="string"/>.
    /// </summary>
    /// <param name="connectionString">The connection string object to convert.</param>
    public static implicit operator string(ConnectionString connectionString) => connectionString.Value;
}