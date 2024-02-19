using UniIdentity.Domain.Clients.ValueObjects;
using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.Clients.Events;

public record ClientCreatedEvent(ClientId Id) : IDomainEvent;