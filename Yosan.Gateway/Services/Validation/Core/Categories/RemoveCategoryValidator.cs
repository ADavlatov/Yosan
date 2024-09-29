using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class RemoveCategoryValidator : AbstractValidator<RemoveCategoryRequest>
{
     public RemoveCategoryValidator()
     {
          RuleFor(x => x.UserId).Empty().WithMessage("UserId can't be empty");
          RuleFor(x => x.Id).NotEmpty().WithMessage("Id can't be empty");
     }
}