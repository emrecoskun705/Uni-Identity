namespace UniIdentity.Domain.Scopes;

public record ScopeId(Guid Value)
{
    public static ScopeId FromValue(Guid value) => new ScopeId(value);

    public static ScopeId New() => FromValue(Guid.NewGuid());
}