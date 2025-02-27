using X.Application.Core.Data;
using X.Application.Core.CQRS;
using X.Domain.Products;

namespace X.Application.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHnalder : ICommandHandler<UpdateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateProductCommandHnalder(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        if (product is null)
        {
            throw new InvalidOperationException($"Product with Id: {request.ProductId} does't exsist");
        }

        product.Update(request.Name, request.Description, request.Price, request.Stock);

        _productRepository.Update(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
