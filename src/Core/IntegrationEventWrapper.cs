using Griffin.Core.Event;

namespace Griffin.Core;

public record IntegrationEventWrapper<TDomainEventType>(TDomainEventType DomainEvent) : IIntegrationEvent
    where TDomainEventType : IDomainEvent;