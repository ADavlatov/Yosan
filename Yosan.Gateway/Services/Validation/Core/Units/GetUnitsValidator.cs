using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Units;

public class GetUnitsValidator : AbstractValidator<GetUnitsRequest>
{
    public GetUnitsValidator()
    {
        RuleFor(x => x.CategoryId).Empty().WithMessage("CategoryId can't be empty");
    }
}