namespace UniIdentity.Application.Contracts.Sessions;

public record SessionKey
{
    public string Key { get; init; }
    public DateTime? StartDate { get; init; }

    private SessionKey(string key, DateTime? startDate)
    {
        Key = key;
        StartDate = startDate;
    }

    public static SessionKey FromKey(string key) => new(key, null);

    public static SessionKey Create(string key, DateTime dateTime) => new(key, dateTime);

    public virtual bool Equals(SessionKey? other)
    {
        if (other == null)
            return false;

        return Key.Equals(other.Key);
    }

    public override int GetHashCode() => Key.GetHashCode() * 37;
}