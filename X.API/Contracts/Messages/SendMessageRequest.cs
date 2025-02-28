namespace X.API.Contracts.Messages;

public sealed record SendMessageRequest(
    Guid SenderId,
    Guid ReceiverId,
    string Content);