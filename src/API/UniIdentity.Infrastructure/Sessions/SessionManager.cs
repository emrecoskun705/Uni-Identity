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

    public async Task<SessionKey> CreateUserSession(UserId userId)
    {
        var sessionUtcStartDate = _timeProvider.GetUtcNow();
        var sessionKey = SessionKey.Create(Guid.NewGuid().ToString(), sessionUtcStartDate.DateTime);

        var existingSessions = await GetUserSessionsAsync(userId.ToString());
        
        existingSessions.Add(sessionKey);

        await _cache.SetAsync(UserSessionCacheKey(userId.ToString()), SerializeSessions(existingSessions));

        return sessionKey;
    }

    public async Task RemoveUserSession(UserId userId, SessionKey sessionKey)
    {
        var existingSessions = await GetUserSessionsAsync(userId.ToString());
        existingSessions.Remove(sessionKey);
        await _cache.SetAsync(UserSessionCacheKey(userId.ToString()), SerializeSessions(existingSessions));
    }

    public async Task<bool> CheckUserSessionExists(UserId userId, SessionKey sessionKey)
    {
        var existingSessions = await GetUserSessionsAsync(userId.ToString());
        return existingSessions.Exists(x => x.Key == sessionKey.Key);
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