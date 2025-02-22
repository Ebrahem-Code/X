using X.Application.Core.Masseges;

namespace X.Application.Users.Commands.DeleteUser;

public sealed record DeleteUserCommand(Guid UserId) : ICommand<Guid>;
