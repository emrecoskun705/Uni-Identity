using UniIdentity.Domain.Credentials.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Credentials;

public class PasswordCredential : Credential
{
    public PasswordCredential(CredentialId id, UserId userId, DateTimeOffset createdDateTime, string? secretData, string? credentialData, short priority) : base(id, userId, createdDateTime, secretData, credentialData, priority)
    {
        Type = CredentialType.Password;
    }
}