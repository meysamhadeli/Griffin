using Griffin.Core.Event;

namespace Griffin.Core.EventBus.Messages;

public record UserCreated(Guid Id, string Name, string PassportNumber) : IIntegrationEvent;