using X.Application.Core.CQRS;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<User>;
