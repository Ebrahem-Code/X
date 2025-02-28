using Microsoft.EntityFrameworkCore;
using X.Application.Core.Data;
using X.Domain.Messages;

namespace X.Infrastructure.Repositories;

internal class MessageRepository(IDbContext dbContext) : IMessageRepository
{
    public async Task AddAsync(Message message, CancellationToken cancellationToken)
    {
        await dbContext.Set<Message>().AddAsync(message, cancellationToken);
    }

    public void Update(Message message)
    {
        dbContext.Set<Message>().Update(message);
    }

    public void Delete(Message message)
    {
        dbContext.Set<Message>().Remove(message);
    }

    public async Task<Message?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Message>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Message>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Message>().Where(x => x.SenderId == userId || x.ReceiverId == userId).ToListAsync(cancellationToken);
    }
}
