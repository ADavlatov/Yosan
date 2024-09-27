using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using Yosan.Core.Contexts;
using Yosan.Core.Models;

namespace Yosan.Core.Services;

public class CategoryService(CoreContext db)
{
    public async Task<AddCategoryResponse> AddCategory(AddCategoryRequest request)
    {
        var category =
            await db.Categories.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Name == request.Name);

        if (category != null)
        {
            return new AddCategoryResponse { IsSucceed = false, Status = 400, Error = "Category already exists" };
        }

        await db.Categories.AddAsync(new Category
        {
            Name = request.Name, UserId = request.UserId, Type = request.Type,
            Units = new List<CategoryUnit>()
        });
        await db.SaveChangesAsync();

        return new AddCategoryResponse { IsSucceed = true, Status = 200, Error = "" };
    }

    public async Task<GetCategoriesResponse> GetCategories(GetCategoriesRequest request)
    {
        RepeatedField<CategoryObject> categoriesRepeatedField = new RepeatedField<CategoryObject>();
        RepeatedField<UnitObject> unitsRepeatedField = new RepeatedField<UnitObject>();
        var categories = await db.Categories.Where(x => x.UserId == request.UserId).Include(x => x.Units).ToListAsync();

        if (!categories.Any())
        {
            return new GetCategoriesResponse { IsSucceed = false, Status = 400, Error = "UserId failed" };
        }
        //@TODO переписать это непотребство
        foreach (var category in categories)
        {
            foreach (var unit in category.Units)
            {
                unitsRepeatedField.Add(new UnitObject
                    { Name = unit.Name, Sum = unit.Sum.ToString(), UserId = unit.UserId, Date = unit.Date.ToString() });
            }

            categoriesRepeatedField.Add(new CategoryObject
            {
                Name = category.Name, UserId = category.UserId, Type = category.Type, Units = { unitsRepeatedField }
            });
            unitsRepeatedField.Clear();
        }

        return new GetCategoriesResponse { IsSucceed = true, Status = 200, Error = "", Categories = { categoriesRepeatedField }};
    }

    public async Task<RemoveCategoryResponse> RemoveCategory(RemoveCategoryRequest request)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id.ToString() == request.Id);

        if (category == null)
        {
            return new RemoveCategoryResponse{ IsSucceed = false, Status = 400, Error = "Category not found" };
        }

        db.Categories.Remove(category);
        await db.SaveChangesAsync();
        
        return new RemoveCategoryResponse{ IsSucceed = true, Status = 200, Error = "" };
    }
}