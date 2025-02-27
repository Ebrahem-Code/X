using MediatR;

namespace X.Application.Core.CQRS;

public interface ICommand : IRequest
{

}

public interface ICommand<out TResult> : IRequest<TResult>
    where TResult : notnull
{

}
