using X.Domain.Core.BaseEntity;

namespace X.Domain.Notifications;

public sealed class Notification : AggregateRoot
{
    private Notification(Guid userId, string message)
        : base(Guid.NewGuid())
    {
        Message = message;
    }

    private Notification() { }

    public Guid UserId { get; }
    public string Message { get; } = default!;

    public static Notification Create(Guid userId, string message)
    {
        return new Notification(userId, message);
    }
}
