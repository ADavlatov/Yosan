using Google.Protobuf.Collections;
using Microsoft.EntityFrameworkCore;
using Yosan.Core.Contexts;
using Yosan.Core.Models;

namespace Yosan.Core.Services;

public class UnitService(CoreContext db)
{
    public async Task<AddUnitResponse> Add(AddUnitRequest request)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == request.CategoryId);

        if (category == null)
        {
            return new AddUnitResponse { IsSucceed = false, Status = 400, Error = "Category not found" };
        }

        await db.CategoryUnits.AddAsync(new CategoryUnit
        {
            Name = request.Name, Sum = float.Parse(request.Sum),
            Date = DateOnly.Parse(request.Date), Category = category
        });
        await db.SaveChangesAsync();

        return new AddUnitResponse { IsSucceed = true, Status = 200, Error = "" };
    }

    public async Task<GetUnitsResponse> Get(GetUnitsRequest request)
    {
        var category = await db.Categories.FirstOrDefaultAsync(x => x.Id.ToString() == request.CategoryId);

        if (category == null)
        {
            return new GetUnitsResponse { IsSucceed = false, Status = 400, Error = "Category not found" };
        }

        RepeatedField<UnitObject> units = new RepeatedField<UnitObject>();

        foreach (var unit in db.CategoryUnits.Where(x => x.Category == category))
        {
            units.Add(new UnitObject
                { Id = unit.Id.ToString(), Name = unit.Name, Sum = unit.Sum.ToString(), Date = unit.Date.ToString() });
        }
        
        return new GetUnitsResponse{ IsSucceed = true, Status = 200, Error = "", Units = { units }};
    }
}