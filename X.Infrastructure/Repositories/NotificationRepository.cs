using Microsoft.EntityFrameworkCore;
using X.Application.Core.Data;
using X.Domain.Notifications;

namespace X.Infrastructure.Repositories;

internal sealed class NotificationRepository(IDbContext dbContext) : INotificationRepository
{
    public async Task AddAsync(Notification notification, CancellationToken cancellationToken)
    {
        await dbContext.Set<Notification>().AddAsync(notification, cancellationToken);
    }

    public async Task<List<Notification>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<Notification>().ToListAsync(cancellationToken);
    }

    public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Notification>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Notification>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Notification>().Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }
}
