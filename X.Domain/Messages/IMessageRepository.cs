namespace X.Domain.Messages;

public interface IMessageRepository 
{
    // Commands
    Task AddAsync(Message message, CancellationToken cancellationToken);
    void Update(Message message);
    void Delete(Message message);

    // Queries.
    Task<List<Message>> GetAllAsync(CancellationToken cancellationToken);
    Task<Message?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Message>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
