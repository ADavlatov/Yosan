using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class DepositCategoryValidator : AbstractValidator<DepositCategoryRequest>
{
    public DepositCategoryValidator()
    {
        RuleFor(x => x).Empty().WithMessage("UserId can't be empty");
        RuleFor(x => x.Sum).Must(x => float.TryParse(x, out _)).WithMessage("Sum can't be parsed");
    }
}