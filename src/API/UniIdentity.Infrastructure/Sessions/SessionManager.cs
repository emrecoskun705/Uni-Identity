using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using UniIdentity.Application.Contracts.Sessions;
using UniIdentity.Domain.Users;

namespace UniIdentity.Infrastructure.Sessions;

internal sealed class SessionManager : ISessionManager
{
    private readonly IDistributedCache _cache;
    private readonly TimeProvider _timeProvider;

    private static Func<string, string> UserSessionCacheKey => (userId) => $"user_sessions_{userId}";

    public SessionManager(IDistributedCache cache, TimeProvider timeProvider)
    {
        _cache = cache;
        _timeProvider = timeProvider;
    }

    public async Task<SessionKey> CreateUserSession(User user)
    {
        var sessionUtcStartDate = _timeProvider.GetUtcNow();
        var sessionKey = new SessionKey(Guid.NewGuid().ToString(), sessionUtcStartDate.DateTime);

        var existingSessions = await GetUserSessionsAsync(user.Id.Value.ToString());
        
        existingSessions.Add(sessionKey);

        await _cache.SetAsync(UserSessionCacheKey(user.Id.Value.ToString()), SerializeSessions(existingSessions));

        return sessionKey;
    }
    
    private async Task<List<SessionKey>> GetUserSessionsAsync(string userId)
    {
        var sessionsData = await _cache.GetStringAsync(UserSessionCacheKey(userId));
        return sessionsData != null ? DeserializeSessions(sessionsData) :  new List<SessionKey>();
    }

    private byte[] SerializeSessions(List<SessionKey> sessions)
    {
        return JsonSerializer.SerializeToUtf8Bytes(sessions);
    }

    private List<SessionKey> DeserializeSessions(string sessionsData)
    {
        return JsonSerializer.Deserialize<List<SessionKey>>(sessionsData)!;
    }
}