using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Credentials.Services;

public interface IPasswordHasher
{
    string HashPassword(Password password);
}