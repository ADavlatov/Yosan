using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class GetCategoriesValidator : AbstractValidator<GetCategoriesRequest>
{
    public GetCategoriesValidator()
    {
        RuleFor(x => x.UserId).Empty().WithMessage("UserId can't be empty");
    }
}