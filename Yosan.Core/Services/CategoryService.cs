using System.IdentityModel.Tokens.Jwt;
using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using Yosan.Core.Contexts;
using Yosan.Core.Models;

namespace Yosan.Core.Services;

public class CategoryService(CoreContext db)
{
    public async Task<AddCategoryResponse> Add(AddCategoryRequest request)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var userId = jwtHandler.ReadJwtToken(request.AccessToken).Claims.First().ToString();
        var category =
            await db.Categories.FirstOrDefaultAsync(x =>
                x.UserId == userId &&
                x.Name == request.Name);

        if (category != null)
        {
            return new AddCategoryResponse { IsSucceed = false, Status = 400, Error = "Category already exists" };
        }

        await db.Categories.AddAsync(new Category
        {
            Name = request.Name, UserId = userId, Type = request.Type,
            Units = new List<CategoryUnit>()
        });
        await db.SaveChangesAsync();

        return new AddCategoryResponse { IsSucceed = true, Status = 200, Error = "" };
    }

    public async Task<GetCategoriesResponse> Get(GetCategoriesRequest request)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var userId = jwtHandler.ReadJwtToken(request.AccessToken).Claims.First().ToString();
        var categoriesRepeatedField = new RepeatedField<CategoryObject>();
        var categories = await db.Categories.Where(x => x.UserId == userId).Include(x => x.Units).ToListAsync();

        if (!categories.Any())
        {
            return new GetCategoriesResponse { IsSucceed = false, Status = 400, Error = "Categories not found" };
        }

        foreach (var category in categories)
        {
            categoriesRepeatedField.Add(new CategoryObject
            {
                Id = category.Id.ToString(),
                Name = category.Name, UserId = category.UserId, Type = category.Type
            });
        }

        return new GetCategoriesResponse
            { IsSucceed = true, Status = 200, Error = "", Categories = { categoriesRepeatedField } };
    }

    public async Task<DepositCategoryResponse> Deposit(DepositCategoryRequest request)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == request.CategoryId);

        if (category == null)
        {
            return new DepositCategoryResponse { IsSucceed = false, Status = 400, Error = "Category not found" };
        }

        if (category.Type != 2)
        {
            return new DepositCategoryResponse { IsSucceed = false, Status = 400, Error = "Wrong category type" };
        }

        category.Sum += float.Parse(request.Sum);
        db.Categories.Update(category);
        await db.SaveChangesAsync();

        return new DepositCategoryResponse { IsSucceed = true, Status = 200, Error = "" };
    }

    public async Task<RemoveCategoryResponse> Remove(RemoveCategoryRequest request)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var category =
            await db.Categories.FirstOrDefaultAsync(x =>
                x.UserId == jwtHandler.ReadJwtToken(request.AccessToken).Claims.First().ToString() &&
                x.Id.ToString() == request.CategoryId);

        if (category == null)
        {
            return new RemoveCategoryResponse { IsSucceed = false, Status = 400, Error = "Category not found" };
        }

        db.Categories.Remove(category);
        await db.SaveChangesAsync();

        return new RemoveCategoryResponse { IsSucceed = true, Status = 200, Error = "" };
    }
}