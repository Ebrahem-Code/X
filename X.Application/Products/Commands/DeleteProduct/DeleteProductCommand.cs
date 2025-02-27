using X.Application.Core.CQRS;

namespace X.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand<Guid>;
