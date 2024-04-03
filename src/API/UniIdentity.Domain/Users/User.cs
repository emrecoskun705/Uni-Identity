using UniIdentity.Domain.Common;
using UniIdentity.Domain.Realms;
using UniIdentity.Domain.Shared;
using UniIdentity.Domain.Users.Events;
using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Users;

/// <summary>
/// Represents a user within the UniIdentity domain, associated with a specific realm.
/// </summary>
public sealed class User : AggregateRoot
{
    /// <summary>
    /// Gets the unique identifier of the user.
    /// </summary>
    public UserId Id { get; init; }

    /// <summary>
    /// Gets the email address of the user.
    /// </summary>
    public Email? Email { get; private set; }

    /// <summary>
    /// Gets the normalized form of the email address.
    /// </summary>
    public NormalizedEmail? NormalizedEmail { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the email address of the user has been verified.
    /// </summary>
    public bool EmailVerified { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the user is active.
    /// </summary>
    public bool Active { get; private set; }

    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    public Username Username { get; private set; }

    /// <summary>
    /// Gets the normalized form of the username.
    /// </summary>
    public NormalizedUsername NormalizedUsername { get; private set; }

    /// <summary>
    /// Gets the date and time when the user was created.
    /// </summary>
    public DateTimeOffset CreatedDateTime { get; private set; }

    /// <summary>
    /// Gets the date and time when the user was last updated.
    /// </summary>
    public DateTimeOffset? UpdatedDateTime { get; private set; }

    /// <summary>
    /// Gets the identifier of the realm to which the user belongs.
    /// </summary>
    public RealmId RealmId { get; private set; }

    /// <summary>
    /// Gets the identity identifier associated with the user.
    /// </summary>
    public IdentityId IdentityId { get; private set; }
    
    /// <summary>
    /// Creates a new user with the specified email, username, password, and creation time.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <param name="username">The username of the user.</param>
    /// <param name="timeProvider">The time provider service used for obtaining the current time.</param>
    /// <returns>A new instance of <see cref="User"/>.</returns>
    public static User Create(Email email, Username username, TimeProvider timeProvider)
    {
        var user = new User(UserId.New(), email, username, timeProvider.GetUtcNow(), IdentityId.New());

        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }

    /// <summary>
    /// Verifies the email address of the user and returns a result indicating the outcome of the operation.
    /// </summary>
    /// <returns>A <see cref="Result"/> indicating whether the email verification was successful or not.</returns>
    public Result VerifyEmail()
    {
        if (EmailVerified)
            Result.Failure(DomainErrors.UserErrors.EmailAlreadyVerified);

        EmailVerified = true;
        return Result.Success();
    }
    
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
    
}