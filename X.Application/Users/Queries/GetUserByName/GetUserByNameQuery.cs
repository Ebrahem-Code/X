using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Queries.GetUserByName;

public sealed record GetUserByNameQuery(string Name) : IQuery<User>;
