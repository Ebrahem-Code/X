using X.Application.Core.Masseges;

namespace X.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{

    public Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
