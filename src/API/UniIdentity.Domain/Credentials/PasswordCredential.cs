using UniIdentity.Domain.Credentials.ValueObjects;
using UniIdentity.Domain.Users;

namespace UniIdentity.Domain.Credentials;

public sealed class PasswordCredential : Credential
{
    private PasswordCredential(CredentialId id, UserId userId, DateTimeOffset createdDateTime, string? secretData, string? credentialData, short priority) : base(id, userId, createdDateTime, secretData, credentialData, priority)
    {
        Type = CredentialType.Password;
    }

    public static PasswordCredential Create(UserId userId, DateTimeOffset createdDateTime, string password)
    {
        var secretData = PasswordManager.HashPassword(password);
        
        var credential = new PasswordCredential(CredentialId.New(), userId, createdDateTime, secretData, null, 10);

        return credential;
    }

    public bool VerifyPassword(string password)
    {
        return PasswordManager.VerifyHashedPassword(SecretData!, password);
    }
    
}