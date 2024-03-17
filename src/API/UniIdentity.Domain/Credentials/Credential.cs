using UniIdentity.Domain.Common;
using UniIdentity.Domain.Credentials.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Credentials;

public abstract class Credential : BaseEntity
{
    public CredentialId Id { get; private set; }
    public UserId UserId { get; private set; }
    public CredentialType Type { get; init; }
    public DateTimeOffset CreatedDateTime { get; private set; }
    public string? SecretData { get; private set; }
    public string? CredentialData { get; private set; }
    public Int16 Priority { get; private set; }
    
    protected Credential(CredentialId id, UserId userId, DateTimeOffset createdDateTime, string? secretData, string? credentialData, short priority)
    {
        Id = id;
        UserId = userId;
        CreatedDateTime = createdDateTime;
        SecretData = secretData;
        CredentialData = credentialData;
        Priority = priority;
    }
    
}