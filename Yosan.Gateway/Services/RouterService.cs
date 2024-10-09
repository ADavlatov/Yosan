using System.Text.Json;
using Google.Protobuf;

namespace Yosan.Gateway.Services;

public class RouterService
{
    public async Task<IBufferMessage?> Execute(string path, string json)
    {
        var methods = new GrpcMethods();

        switch (path)
        {
            case "api/v1/auth/signIn":
                var signInRequest = JsonSerializer.Deserialize<SignInRequest>(json);
                if (signInRequest != null) await methods.SignIn(signInRequest);
                break;
            case "api/v1/auth/logIn":
                var logInRequest = JsonSerializer.Deserialize<LogInRequest>(json);
                if (logInRequest != null) await methods.LogIn(logInRequest);
                break;
            case "api/v1/auth/validate":
                var validateRequest = JsonSerializer.Deserialize<TokenValidationRequest>(json);
                if (validateRequest != null) await methods.Validate(validateRequest);
                break;
            case "api/v1/auth/refresh":
                var refreshRequest = JsonSerializer.Deserialize<RefreshTokenRequest>(json);
                if (refreshRequest != null) await methods.Refresh(refreshRequest);
                break;
            case "api/v1/core/addCategory":
                var addCategoryRequest = JsonSerializer.Deserialize<AddCategoryRequest>(json);
                if (addCategoryRequest != null) await methods.AddCategory(addCategoryRequest);
                break;
            case "api/v1/core/getCategories":
                var getCategoriesRequest = JsonSerializer.Deserialize<GetCategoriesRequest>(json);
                if (getCategoriesRequest != null) await methods.GetCategories(getCategoriesRequest);
                break;
            case "api/v1/core/updateCategory":
                var updateCategoryRequest = JsonSerializer.Deserialize<DepositCategoryRequest>(json);
                if (updateCategoryRequest != null) await methods.Deposit(updateCategoryRequest);
                break;
            case "api/v1/core/deleteCategory":
                var deleteCategoryRequest = JsonSerializer.Deserialize<RemoveCategoryRequest>(json);
                if (deleteCategoryRequest != null) await methods.Remove(deleteCategoryRequest);
                break;
            case "api/v1/core/addUnit":
                var addUnitRequest = JsonSerializer.Deserialize<AddUnitRequest>(json);
                if (addUnitRequest != null) await methods.AddUnit(addUnitRequest);
                break;
            case "api/v1/core/getUnits":
                var getUnitsRequest = JsonSerializer.Deserialize<GetUnitsRequest>(json);
                if (getUnitsRequest != null) await methods.GetUnits(getUnitsRequest);
                break;
        }

        return null;
    }
}