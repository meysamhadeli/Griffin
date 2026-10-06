using Griffin.Core.Event;
using MediatR;

namespace Griffin.EventStoreDB.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}