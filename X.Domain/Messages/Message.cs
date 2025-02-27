using X.Domain.Core.BaseEntity;
using X.Domain.Messages.Events;

namespace X.Domain.Messages;

public sealed class Message : AggregateRoot
{
    private Message(Guid senderId, Guid receiverId, string content)
        : base()
    {
        SenderId = senderId;
        ReceiverId = receiverId;
        Content = content;
        SentAt = DateTime.UtcNow;
    }

    private Message() { }

    public Guid SenderId { get; }
    public Guid ReceiverId { get; }
    public string Content { get; private set; } = default!;
    public DateTime SentAt { get; }
    public bool IsRead { get; private set; }

    public static Message Create(Guid senderId, Guid receiverId, string content)
    {
        Message message = new Message(senderId, receiverId, content);

        // Raise DomainEvent.
        message.AddDomainEvent(new MessageSendDomainEvent(message));

        return message;
    }

    public void UpdateContent(string newContent)
    {
        Content = newContent;

        // Raise DomainEvent.
        this.AddDomainEvent(new MessageUpdatedDomainEvent(this));
    }

    public void MarkAsRead()
    {
        IsRead = true;

        // Raise DomainEvent.
        this.AddDomainEvent(new MessageReadDomainEvent(this));
    }

    public void Delete()
    {

    }
}
