using X.Application.Core.CQRS;
using X.Application.Core.Data;
using X.Domain.Messages;

namespace X.Application.Messages.Commands.DeleteMessage;

public sealed record DeleteMessageCommand(Guid MessageId) : ICommand;

internal sealed class DeleteMessageCommandHandler : ICommandHandler<DeleteMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        if (message == null)
        {
            throw new Exception("Message not found");
        }

        _messageRepository.Delete(message);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
