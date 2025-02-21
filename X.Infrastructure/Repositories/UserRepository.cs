using Microsoft.EntityFrameworkCore;
using X.Application.Core.Data;
using X.Domain.Users;

namespace X.Infrastructure.Repositories;

internal sealed class UserRepository(IDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.Set<User>().AddAsync(user, cancellationToken);
    }

    public void UpdateAsync(User user)
    {
        dbContext.Set<User>().Update(user);
    }

    public void DeleteAsync(User user)
    {
        dbContext.Set<User>().Remove(user);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<User>().ToListAsync(cancellationToken);  
    }

    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }
}
