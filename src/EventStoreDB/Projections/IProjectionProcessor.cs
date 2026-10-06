using Griffin.EventStoreDB.Events;
using MediatR;

namespace Griffin.EventStoreDB.Projections;

public interface IProjectionProcessor
{
    Task ProcessEventAsync<T>(StreamEvent<T> streamEvent, CancellationToken cancellationToken = default)
        where T : INotification;
}