using MediatR;

namespace UniIdentity.Domain.Common;

/// <summary>
/// Represents the interface for an event that is raised within the domain.
/// </summary>
public interface IDomainEvent : INotification
{
}