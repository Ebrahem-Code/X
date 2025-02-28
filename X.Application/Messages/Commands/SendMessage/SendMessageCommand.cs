using X.Application.Core.CQRS;
using X.Application.Core.Data;
using X.Domain.Messages;

namespace X.Application.Messages.Commands.SendMessage;

public sealed record SendMessageCommand(
    Guid SenderId,
    Guid ReceiverId,
    string Content) : ICommand<Guid>;

internal sealed class SendMessageCommandHandler : ICommandHandler<SendMessageCommand, Guid>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SendMessageCommandHandler(IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        Message message = Message.Create(request.SenderId, request.ReceiverId, request.Content);

        await _messageRepository.AddAsync(message, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
