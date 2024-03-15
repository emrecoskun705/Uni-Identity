using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Credentials.Services;

public interface IPasswordVerifier
{
    bool VerifyHashedPassword(string hashedPassword, Password password);
}