using UniIdentity.Domain.Common;
using UniIdentity.Domain.Credentials.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Credentials;

public abstract class Credential : BaseEntity<CredentialId>
{
    public UserId UserId { get; set; }
    public CredentialType Type { get; init; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public string? SecretData { get; set; }
    public string? CredentialData { get; set; }
    public Int16 Priority { get; set; }

    protected Credential(CredentialId id, UserId userId, DateTimeOffset createdDateTime, string? secretData, string? credentialData, short priority) : base(id)
    {
        UserId = userId;
        CreatedDateTime = createdDateTime;
        SecretData = secretData;
        CredentialData = credentialData;
        Priority = priority;
    }
    
}