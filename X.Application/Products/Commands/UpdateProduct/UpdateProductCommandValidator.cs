using FluentValidation;

namespace X.Application.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId).NotNull().NotEmpty();

        RuleFor(x => x.Name).NotNull().NotEmpty();

        RuleFor(x => x.Description).NotNull().NotEmpty();

        RuleFor(x => x.Price).NotNull().NotEmpty();

        RuleFor(x => x.Stock).NotNull().NotEmpty();
    }
}
