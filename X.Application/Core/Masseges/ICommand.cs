using MediatR;

namespace X.Application.Core.Masseges;

public interface ICommand : IRequest
{

}

public interface ICommand<out TResult> : IRequest<TResult>
    where TResult : notnull
{

}
