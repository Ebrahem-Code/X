using X.Application.Core.Masseges;

namespace X.Application.Users.Commands.UpdateUser;

public sealed record UpdateUserCommand(
    Guid UserId,
    string FirstName, 
    string LastName): ICommand<Guid>;
