using Grpc.Core;
using Yosan.Core.Contexts;

namespace Yosan.Core.Services;

public class CoreService(CoreContext db) : Core.CoreBase
{
    public override Task<AddIncomeResponse> AddIncome(AddIncomeRequest request, ServerCallContext context)
    {
        return base.AddIncome(request, context);
    }

    public override Task<DeleteIncomeResponse> DeleteIncome(DeleteIncomeRequest request, ServerCallContext context)
    {
        return base.DeleteIncome(request, context);
    }

    public override Task<AddExpenseResponse> AddExpense(AddExpenseRequest request, ServerCallContext context)
    {
        return base.AddExpense(request, context);
    }

    public override Task<DeleteExpenseResponse> DeleteExpense(DeleteExpenseRequest request, ServerCallContext context)
    {
        return base.DeleteExpense(request, context);
    }

    public override Task<AddSavingResponse> AddSaving(AddSavingRequest request, ServerCallContext context)
    {
        return base.AddSaving(request, context);
    }

    public override Task<DeleteSavingResponse> DeleteSaving(DeleteSavingRequest request, ServerCallContext context)
    {
        return base.DeleteSaving(request, context);
    }
}