using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserByEmail;

public sealed record GetUserByEmailQuery(string Email) : IQuery<User>;
