using Griffin.Core.Event;

namespace Griffin.Core.Contracts.EventBus.Messages;

public record BookingCreated(Guid Id) : IIntegrationEvent;