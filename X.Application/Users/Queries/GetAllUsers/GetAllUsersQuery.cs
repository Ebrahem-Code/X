using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery() : IQuery<List<User>>;
