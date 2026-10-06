using Griffin.Core.Event;

namespace Griffin.Core.EventBus.Messages;

public record BookingCreated(Guid Id) : IIntegrationEvent;