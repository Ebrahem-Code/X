using FluentValidation;

namespace X.Application.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);

        RuleFor(x => x.Description).NotEmpty().MaximumLength(500);

        RuleFor(x => x.Price).GreaterThan(0);

        RuleFor(x => x.Stock).GreaterThan(0);
    }
}
