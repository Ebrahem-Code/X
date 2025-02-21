using X.Application.Core.Masseges;

namespace X.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    string firstName, 
    string lastName): ICommand;
