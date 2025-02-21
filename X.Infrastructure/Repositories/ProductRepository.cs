using Microsoft.EntityFrameworkCore;
using X.Application.Core.Data;
using X.Domain.Products;

namespace X.Infrastructure.Repositories;

internal sealed class ProductRepository(IDbContext dbContext) : IProductRepository
{
    public async Task AddAsync(Product product, CancellationToken cancellationToken)
    {
        await dbContext.Set<Product>().AddAsync(product, cancellationToken);
    }

    public void Update(Product product)
    {
        dbContext.Set<Product>().Update(product);
    }

    public void Delete(Product product)
    {
        dbContext.Set<Product>().Remove(product);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Set<Product>().ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Set<Product>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
