using X.Application.Core.CQRS;
using X.Domain.Messages;

namespace X.Application.Messages.Queries.GetMessagesByUserId;

public sealed record GetMessagesByUserIdQuery(Guid UserId) : IQuery<List<Message>>;

internal sealed class GetMessagesByUserIdQueryHandler : IQueryHandler<GetMessagesByUserIdQuery, List<Message>>
{
    private readonly IMessageRepository _messageRepository;

    public GetMessagesByUserIdQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<Message>> Handle(GetMessagesByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await _messageRepository.GetByUserIdAsync(request.UserId, cancellationToken);
    }
}
