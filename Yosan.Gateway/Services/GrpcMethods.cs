using Grpc.Net.Client;
using Yosan.Gateway.Services.Validation.Auth;
using Yosan.Gateway.Services.Validation.Core.Categories;
using Yosan.Gateway.Services.Validation.Core.Units;

namespace Yosan.Gateway.Services;

public class GrpcMethods
{
    private readonly Auth.AuthClient _authClient;
    private readonly Core.CoreClient _coreClient;

    public GrpcMethods()
    {
        var authChannel = GrpcChannel.ForAddress("https://localhost:7231");
        _authClient = new Auth.AuthClient(authChannel);

        var coreChannel = GrpcChannel.ForAddress("https://localhost:7231");
        _coreClient = new Core.CoreClient(coreChannel);
    }

    public async Task<SignInResponse> SignIn(SignInRequest request)
    {
        var result = await new SignInValidator().ValidateAsync(request);

        if (!result.IsValid)
        {
            return new SignInResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }

        return await _authClient.SignInUserAsync(request);
    }

    public async Task<LogInResponse> LogIn(LogInRequest request)
    {
        var result = await new LogInValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new LogInResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _authClient.LogInUserAsync(request);
    }

    public async Task<TokenValidationResponse> Validate(TokenValidationRequest request)
    {
        var result = await new AccessTokenValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new TokenValidationResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _authClient.ValidateTokenAsync(request);
    }

    public async Task<RefreshTokenResponse> Refresh(RefreshTokenRequest request)
    {
        var result = await new RefreshTokenValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new RefreshTokenResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _authClient.GetAccessTokenAsync(request);
    }

    public async Task<AddCategoryResponse> AddCategory(AddCategoryRequest request)
    {
        var result = await new AddCategoryValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new AddCategoryResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        var userResponse = await _authClient.ValidateTokenAsync(new TokenValidationRequest
            { AccessToken = request.AccessToken });
        
        if (!userResponse.IsSucceed)
        {
            return new AddCategoryResponse { IsSucceed = false, Status = 400, Error = userResponse.Error };
        }
        
        return await _coreClient.AddCategoryAsync(request);
    }

    public async Task<GetCategoriesResponse> GetCategories(GetCategoriesRequest request)
    {
        var result = await new GetCategoriesValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new GetCategoriesResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _coreClient.GetCategoriesAsync(request);
    }

    public async Task<DepositCategoryResponse> Deposit(DepositCategoryRequest request)
    {
        var result = await new DepositCategoryValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new DepositCategoryResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _coreClient.DepositCategoryAsync(request);
    }

    public async Task<RemoveCategoryResponse> Remove(RemoveCategoryRequest request)
    {
        var result = await new RemoveCategoryValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new RemoveCategoryResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _coreClient.RemoveCategoryAsync(request);
    }

    public async Task<AddUnitResponse> AddUnit(AddUnitRequest request)
    {
        var result = await new AddUnitValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new AddUnitResponse
            {
                IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors)
            };
        }
        
        return await _coreClient.AddUnitAsync(request);
    }

    public async Task<GetUnitsResponse> GetUnits(GetUnitsRequest request)
    {
        var result = await new GetUnitsValidator().ValidateAsync(request);
        
        if (!result.IsValid)
        {
            return new GetUnitsResponse
                { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
        }
        
        return await _coreClient.GetUnitsAsync(request);
    }
}
