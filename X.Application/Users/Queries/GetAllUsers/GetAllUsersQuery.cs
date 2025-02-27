using X.Application.Core.CQRS;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetAllUsers;

public sealed record GetAllUsersQuery() : IQuery<List<User>>;
