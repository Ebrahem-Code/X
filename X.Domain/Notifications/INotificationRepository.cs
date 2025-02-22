namespace X.Domain.Notifications;

public interface INotificationRepository
{
    Task AddAsync(Notification notification, CancellationToken cancellationToken);
    Task<List<Notification>> GetAllAsync(CancellationToken cancellationToken);
    Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
