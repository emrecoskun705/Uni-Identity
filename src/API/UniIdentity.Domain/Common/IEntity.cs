namespace UniIdentity.Domain.Common;

public interface IEntity
{
    void ClearDomainEvents();
    IReadOnlyList<IDomainEvent> GetDomainEvents();
}