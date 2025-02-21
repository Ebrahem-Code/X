using X.Application.Core.Data;
using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : ICommand;
