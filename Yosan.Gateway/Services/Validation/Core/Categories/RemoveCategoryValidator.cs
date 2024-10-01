﻿using System.IdentityModel.Tokens.Jwt;
using FluentValidation;

namespace Yosan.Gateway.Services.Validation.Core.Categories;

public class RemoveCategoryValidator : AbstractValidator<RemoveCategoryRequest>
{
    public RemoveCategoryValidator()
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        RuleFor(x => x.AccessToken).Must(x => jwtHandler.CanReadToken(x)).WithMessage("Wrong token");
        RuleFor(x => x.CategoryId).Empty().WithMessage("CategoryId can't be empty");
    }
}