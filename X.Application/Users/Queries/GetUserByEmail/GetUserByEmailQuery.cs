using X.Application.Core.CQRS;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<User>;
