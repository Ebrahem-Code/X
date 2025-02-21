namespace X.Domain.Users;

public interface IUserRepository
{
    Task<User> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task UpdateAsync(User user, CancellationToken cancellationToken);
    Task DeleteAsync(User user, CancellationToken cancellationToken);
}
