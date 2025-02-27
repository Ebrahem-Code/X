using MediatR;

namespace X.Application.Core.CQRS;

public interface IQuery : IRequest
{

}

public interface IQuery<out TResult> : IRequest<TResult>
    where TResult : notnull
{

}
