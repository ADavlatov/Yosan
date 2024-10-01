using System.IdentityModel.Tokens.Jwt;
using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class DepositCategoryValidator : AbstractValidator<DepositCategoryRequest>
{
    public DepositCategoryValidator()
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        RuleFor(x => x.AccessToken).Must(x => jwtHandler.CanReadToken(x)).WithMessage("Wrong token");
        RuleFor(x => x.CategoryId).Empty().WithMessage("CategoryId can't be empty");
        RuleFor(x => x.Sum).Must(x => float.TryParse(x, out _)).WithMessage("Sum can't be parsed");
    }
}