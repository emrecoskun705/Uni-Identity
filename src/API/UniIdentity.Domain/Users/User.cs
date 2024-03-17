using UniIdentity.Domain.Common;
using UniIdentity.Domain.Credentials.Services;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Shared;
using UniIdentity.Domain.Users.Events;
using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Users;

public sealed class User : AggregateRoot
{
    public UserId Id { get; private set; }
    public Email? Email { get; private set; }
    public NormalizedEmail? NormalizedEmail { get; private set; }
    public bool EmailVerified { get; private set; }
    public bool Active { get; private set; }
    public Username Username { get; private set; }
    public NormalizedUsername NormalizedUsername { get; private set; }
    public DateTimeOffset CreatedDateTime { get; private set; }
    public DateTimeOffset? UpdatedDateTime { get; private set; }
    public RealmId RealmId { get; set; }
    public IdentityId IdentityId { get; private set; }
    
    private User(UserId id, Email email, Username username, DateTimeOffset createdDateTime, IdentityId identityId)
    {
        Id = id;
        Email = email;
        NormalizedEmail = NormalizedEmail.Create(email.Value);
        Username = username;
        NormalizedUsername = NormalizedUsername.Create(username.Value);
        EmailVerified = false;
        Active = true;
        CreatedDateTime = createdDateTime;
        IdentityId = identityId;
    }
    
    public static User Create(Email email, Username username, Password password, DateTimeOffset createTime, IPasswordHasher passwordHasher)
    {
        var user = new User(UserId.New(), email, username, createTime, IdentityId.New());

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }

    public Result VerifyEmail()
    {
        if (EmailVerified)
            Result.Failure(DomainErrors.UserErrors.EmailAlreadyVerified);

        EmailVerified = true;
        return Result.Success();
    }
    
}