using UniIdentity.Domain.Credentials.Services;
using UniIdentity.Domain.Credentials.ValueObjects;
using UniIdentity.Domain.Users;
using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Credentials;

public sealed class PasswordCredential : Credential
{
    private PasswordCredential(CredentialId id, UserId userId, DateTimeOffset createdDateTime, string? secretData, string? credentialData, short priority) : base(id, userId, createdDateTime, secretData, credentialData, priority)
    {
        Type = CredentialType.Password;
    }

    public static PasswordCredential Create(UserId userId, DateTimeOffset createdDateTime, string hashedPassword)
    {
        var credential = new PasswordCredential(CredentialId.New(), userId, createdDateTime, hashedPassword, null, 10);

        return credential;
    }

    public bool VerifyPassword(string password, IPasswordVerifier passwordVerifier)
    {
        return passwordVerifier.VerifyHashedPassword(SecretData!, Password.FromValue(password));
    }
    
}