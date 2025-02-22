namespace X.Domain.Orders;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    void Update(Order order);
    void Delete(Order order);
    Task<List<Order>> GetAllAsync(CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
