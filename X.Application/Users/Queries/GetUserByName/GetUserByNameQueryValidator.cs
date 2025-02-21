using FluentValidation;

namespace X.Application.Users.Queries.GetUserByName;

internal sealed class GetUserByNameQueryValidator : AbstractValidator<GetUserByNameQuery>
{
    public GetUserByNameQueryValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}