using FluentValidation;

namespace X.Application.Users.Commands.UpdateUser;

internal sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.firstName).NotNull().NotEmpty();
        RuleFor(x => x.lastName).NotNull().NotEmpty();
    }
}
