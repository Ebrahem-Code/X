using X.Application.Core.Masseges;

namespace X.Application.Products.Commands.DeleteProduct;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand<Guid>;
