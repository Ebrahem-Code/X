using X.Application.Core.Masseges;

namespace X.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand<Guid>;
