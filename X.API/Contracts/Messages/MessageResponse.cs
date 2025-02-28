namespace X.API.Contracts.Messages;

public sealed record MessageResponse(
    Guid Id,
    Guid SenderId,
    Guid ReceiverId,
    string Content,
    DateTime SentAt,
    bool IsRead);