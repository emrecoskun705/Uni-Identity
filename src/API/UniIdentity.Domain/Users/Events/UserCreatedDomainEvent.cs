using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Users.Events;

public record UserCreatedDomainEvent(UserId UserId) : IDomainEvent;
