using Grpc.Core;
using Yosan.Core.Contexts;

namespace Yosan.Core.Services;

public class CoreService(CoreContext db) : Core.CoreBase
{
    public override async Task<AddCategoryResponse> AddCategory(AddCategoryRequest request, ServerCallContext context)
    {
        return await new CategoryService(db).Add(request);
    }

    public override async Task<GetCategoriesResponse> GetCategories(GetCategoriesRequest request, ServerCallContext context)
    {
        return await new CategoryService(db).Get(request);
    }

    public override async Task<DepositCategoryResponse> DepositCategory(DepositCategoryRequest request, ServerCallContext context)
    {
        return await new CategoryService(db).Deposit(request);
    }

    public override async Task<RemoveCategoryResponse> RemoveCategory(RemoveCategoryRequest request, ServerCallContext context)
    {
        return await new CategoryService(db).Remove(request);
    }

    public override async Task<AddUnitResponse> AddUnit(AddUnitRequest request, ServerCallContext context)
    {
        return await new UnitService(db).Add(request);
    }

    public override async Task<GetUnitsResponse> GetUnits(GetUnitsRequest request, ServerCallContext context)
    {
        return await new UnitService(db).Get(request);
    }
}