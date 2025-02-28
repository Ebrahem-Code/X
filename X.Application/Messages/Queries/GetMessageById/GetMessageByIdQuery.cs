using X.Application.Core.CQRS;
using X.Domain.Messages;

namespace X.Application.Messages.Queries.GetMessageById;

public sealed record GetMessageByIdQuery(Guid MessageId) : IQuery<Message>;
