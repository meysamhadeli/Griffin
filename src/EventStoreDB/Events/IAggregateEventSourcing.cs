using Griffin.Core.Event;
using Griffin.Core.Model;

namespace Griffin.EventStoreDB.Events
{
    public interface IAggregateEventSourcing : IProjection, IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents();
    }

    public interface IAggregateEventSourcing<T> : IAggregateEventSourcing, IEntity<T>
    {
    }
}