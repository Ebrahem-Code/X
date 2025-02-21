using MediatR;

namespace X.Application.Core.Masseges;

public interface IQuery : IRequest
{

}

public interface IQuery<out TResult> : IRequest<TResult>
    where TResult : notnull
{

}
