using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserByEmail;

internal sealed class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, User>
{
    public Task<User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
