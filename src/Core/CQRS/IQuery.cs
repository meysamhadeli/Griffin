using MediatR;

namespace Griffin.Core.CQRS;

public interface IQuery<out T> : IRequest<T>
    where T : notnull
{
}