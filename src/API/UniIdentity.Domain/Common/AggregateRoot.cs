namespace UniIdentity.Domain.Common;

public abstract class AggregateRoot : BaseEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    
    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public IReadOnlyList<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents.AsReadOnly();
    }
}