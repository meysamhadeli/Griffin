using Griffin.Core.Event;

namespace Griffin.Core.Contracts.EventBus.Messages;

public record UserCreated(Guid Id, string Name, string PassportNumber) : IIntegrationEvent;