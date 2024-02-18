using UniIdentity.Application.Tokens.Models;
using UniIdentity.Domain.Clients;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Users;

namespace UniIdentity.Application.Tokens.Contracts;

public interface ICreateAccessToken
{
    Task<AccessToken> Create(RealmId realmId, ClientId clientId, UserId userId, CancellationToken cancellationToken = default);
}