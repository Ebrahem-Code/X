using X.Application.Core.CQRS;
using X.Application.Core.Data;
using X.Domain.Messages;

namespace X.Application.Messages.Commands.UpdateMessage;

public sealed record UpdateMessageCommand(
    Guid MessageId,
    string NewContent) : ICommand;

internal sealed class UpdateMessageCommandHandler : ICommandHandler<UpdateMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _messageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        if (message == null)
        {
            throw new Exception("Message not found");
        }

        message.UpdateContent(request.NewContent);

        _messageRepository.Update(message);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
