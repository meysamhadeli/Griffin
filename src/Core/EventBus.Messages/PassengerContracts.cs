using Griffin.Core.Event;

namespace Griffin.Core.EventBus.Messages;

public record PassengerRegistrationCompleted(Guid Id) : IIntegrationEvent;
public record PassengerCreated(Guid Id) : IIntegrationEvent;