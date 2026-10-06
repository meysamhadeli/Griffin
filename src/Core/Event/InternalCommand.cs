using Griffin.Core.CQRS;

namespace Griffin.Core.Event;

public record InternalCommand : IInternalCommand, ICommand;