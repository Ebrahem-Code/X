using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<User>;
