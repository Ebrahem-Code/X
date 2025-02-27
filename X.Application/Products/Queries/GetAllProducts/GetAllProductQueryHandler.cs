using X.Application.Core.CQRS;
using X.Domain.Products;

namespace X.Application.Products.Queries.GetAllProducts;

internal sealed class GetAllProductQueryHandler : IQueryHandler<GetAllProductsQuery, List<Product>>
{
    private readonly IProductRepository _productRepository;
    public GetAllProductQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetAllAsync(cancellationToken);
    }
}   
