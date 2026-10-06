using Griffin.Core.Event;

namespace Griffin.Core;

public interface IEventMapper
{
    IIntegrationEvent? MapToIntegrationEvent(IDomainEvent @event);
    IInternalCommand? MapToInternalCommand(IDomainEvent @event);
}