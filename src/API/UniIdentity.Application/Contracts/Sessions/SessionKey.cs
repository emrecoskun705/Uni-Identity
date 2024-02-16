namespace UniIdentity.Application.Contracts.Sessions;

/// <summary>
/// Represents a unique key associated with a user session. The key value serves as the identifier for the session,
/// ensuring each session has a distinct identifier. Optionally, a start date can be provided to indicate when the session began.
/// </summary>
public record SessionKey
{
    public string Key { get; init; }
    public DateTime? StartDate { get; init; }

    private SessionKey(string key, DateTime? startDate)
    {
        Key = key;
        StartDate = startDate;
    }

    /// <summary>
    /// Creates a session key instance with the specified key value.
    /// </summary>
    /// <param name="key">The unique key value identifying the session.</param>
    /// <returns>A new <see cref="SessionKey"/> instance.</returns>
    public static SessionKey FromKey(string key) => new(key, null);
    
    /// <summary>
    /// Creates a session key instance with the specified key value and start date.
    /// </summary>
    /// <param name="key">The unique key value identifying the session.</param>
    /// <param name="dateTime">The optional start date of the session.</param>
    /// <returns>A new <see cref="SessionKey"/> instance.</returns>
    public static SessionKey Create(string key, DateTime dateTime) => new(key, dateTime);

    public virtual bool Equals(SessionKey? other)
    {
        return other != null && Key.Equals(other.Key);
    }

    public override int GetHashCode() => Key.GetHashCode() * 37;
}