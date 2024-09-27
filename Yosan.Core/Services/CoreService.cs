using Grpc.Core;
using Yosan.Core.Contexts;

namespace Yosan.Core.Services;

public class CoreService(CoreContext db) : Core.CoreBase
{
    public override Task<AddCategoryResponse> AddCategory(AddCategoryRequest request, ServerCallContext context)
    {
        return base.AddCategory(request, context);
    }
}