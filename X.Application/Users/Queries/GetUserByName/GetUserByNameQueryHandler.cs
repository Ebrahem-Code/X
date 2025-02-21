using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserByName;

internal sealed class GetUserByNameQueryHandler : IQueryHandler<GetUserByNameQuery, User>
{
    public Task<User> Handle(GetUserByNameQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
