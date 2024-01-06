﻿using UniIdentity.Domain.Common;
using UniIdentity.Domain.Credentials;
using UniIdentity.Domain.Users.Events;
using UniIdentity.Domain.Users.ValueObjects;

namespace UniIdentity.Domain.Users;

public sealed class User : BaseEntity<UserId>
{
    public Email? Email { get; set; }
    public NormalizedEmail? NormalizedEmail { get; set; }
    public bool EmailVerified { get; set; }
    public bool Active { get; set; }
    public Username Username { get; set; }
    public NormalizedUsername NormalizedUsername { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }
    public DateTimeOffset? UpdatedDateTime { get; set; }
    public IEnumerable<UserRole> UserRoles { get; }
    public IEnumerable<Credential> Credentials { get; }
    
    public IdentityId IdentityId { get; private set; }

    private User(UserId id, Email email, Username username, DateTimeOffset createdDateTime, IdentityId identityId) : base(id)
    {
        Email = email;
        NormalizedEmail = NormalizedEmail.Create(email.Value);
        Username = username;
        NormalizedUsername = NormalizedUsername.Create(username.Value);
        EmailVerified = false;
        Active = true;
        CreatedDateTime = createdDateTime;
        IdentityId = identityId;
    }
    
    public static User Create(Email email, Username username, Password password, DateTimeOffset createTime)
    {
        var user = new User(UserId.New(), email, username, createTime, IdentityId.New());
        user.AddDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
    
}