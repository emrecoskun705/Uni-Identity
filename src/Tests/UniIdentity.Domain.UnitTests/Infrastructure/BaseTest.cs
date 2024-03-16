using UniIdentity.Domain.Common;

namespace UniIdentity.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    
    public static T AssertDomainEventWasPublished<T>(AggregateRoot entity)
        where T : IDomainEvent
    {
        var domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault();

        if (domainEvent == null)
            throw new Exception($"{typeof(T).Name} was not published");
        
        return domainEvent;
    }
    
}