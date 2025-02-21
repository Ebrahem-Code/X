using Microsoft.EntityFrameworkCore;

namespace X.Application.Core.Data;

public interface IDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
}
