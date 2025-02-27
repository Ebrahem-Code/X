using X.Application.Core.CQRS;
using X.Domain.Products;

namespace X.Application.Products.Queries.GetAllProducts;

public sealed record GetAllProductsQuery() : IQuery<List<Product>>;
