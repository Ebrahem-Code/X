using Microsoft.EntityFrameworkCore;
using X.Application.Core.Data;
using X.Domain.Orders;

namespace X.Infrastructure.Repositories;

internal class OrderRepository(IDbContext dbContext) : IOrderRepository
{
    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        await dbContext.Set<Order>().AddAsync(order, cancellationToken);
    }

    public void Delete(Order order)
    {
        dbContext.Set<Order>().Remove(order);
    }

    public async Task<List<Order>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<Order>().ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Order>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Order>().Where(x => x.UserId == userId).ToListAsync(cancellationToken);
    }

    public void Update(Order order)
    {
        dbContext.Set<Order>().Update(order);
    }
}
