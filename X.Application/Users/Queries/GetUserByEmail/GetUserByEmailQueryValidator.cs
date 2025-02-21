using FluentValidation;

namespace X.Application.Users.Queries.GetUserByEmail;

internal sealed class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().NotNull();
    }
}
