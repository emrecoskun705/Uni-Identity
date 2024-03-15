namespace UniIdentity.Domain.Users.ValueObjects;

public sealed record Password
{
    private Password(string value) => Value = value;

    public string Value { get; init; }
    
    public static Password FromValue(string value) => new Password(value);

    public static implicit operator string(Password password) => password.Value;
}