using FluentValidation;

namespace X.Application.Users.Queries.GetUserById;

internal sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull().NotEmpty();
    }
}
