using FluentValidation;

namespace X.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty();

        RuleFor(x => x.LastName).NotNull().NotEmpty();

        RuleFor(x => x.Email).NotNull().NotEmpty();

        RuleFor(x => x.Password).NotNull().NotEmpty();
    }
}
