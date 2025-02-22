using X.Application.Core.Data;
using X.Application.Core.Masseges;
using X.Domain.Users;

namespace X.Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Guid>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException($"User with Id: {request.UserId} does't exsist");
        }

        user.Update(request.FirstName, request.LastName);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
