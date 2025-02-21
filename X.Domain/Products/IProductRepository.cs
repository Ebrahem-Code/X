namespace X.Domain.Products;

public interface IProductRepository
{
    Task<Product> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Product product, CancellationToken cancellationToken);
    void Update(Product product);
    void Delete(Product product);
}
