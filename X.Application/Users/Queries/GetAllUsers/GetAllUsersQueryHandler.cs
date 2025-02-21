using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetAllUsers;

internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, List<User>>
{
    public Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
