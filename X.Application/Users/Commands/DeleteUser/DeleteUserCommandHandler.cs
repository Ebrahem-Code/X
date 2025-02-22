using X.Application.Core.Data;
using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Commands.DeleteUser;

internal sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException($"User with Id: {request.UserId} does't exsist");
        }

        _userRepository.Delete(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return request.UserId;
    }
}
