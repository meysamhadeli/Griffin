using Griffin.Core.Event;

namespace Griffin.Core.Contracts.EventBus.Messages;

public record PassengerRegistrationCompleted(Guid Id) : IIntegrationEvent;
public record PassengerCreated(Guid Id) : IIntegrationEvent;