using UniIdentity.Domain.Clients.ValueObjects;

// ReSharper disable once CheckNamespace
namespace UniIdentity.Domain;

public static partial class CacheKeys
{
    public static readonly Func<ClientId, string> ClientScopesByClientId =
        (clientId) => $"client-scopes-{clientId}";
}