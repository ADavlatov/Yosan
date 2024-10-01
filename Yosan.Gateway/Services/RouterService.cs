using Grpc.Net.Client;
using Yosan.Gateway.Services.Validation.Auth;
using Yosan.Gateway.Services.Validation.Core.Categories;
using Yosan.Gateway.Services.Validation.Core.Units;

namespace Yosan.Gateway.Services;

public class RouterService
{
    private readonly Auth.AuthClient _authClient;
    private readonly Core.CoreClient _coreClient;

    public RouterService()
    {
        var authChannel = GrpcChannel.ForAddress("https://localhost:7231");
        _authClient = new Auth.AuthClient(authChannel);

        var coreChannel = GrpcChannel.ForAddress("");
        _coreClient = new Core.CoreClient(coreChannel);
    }

    public void Execute(WebApplication app)
    {
        app.MapPost("/api/v1/auth/signIn",
            async (SignInRequest request) =>
            {
                var result = await new SignInValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new SignInResponse
                        { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
                }

                return await _authClient.SignInUserAsync(request);
            });


        app.MapPost("api/v1/auth/logIn",
            async (LogInRequest request) =>
            {
                var result = await new LogInValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new LogInResponse
                        { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
                }

                return await _authClient.LogInUserAsync(request);
            });

        app.MapPost("api/v1/auth/validate",
            async (TokenValidationRequest request) =>
            {
                var result = await new AccessTokenValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new TokenValidationResponse
                        { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
                }

                return await _authClient.ValidateTokenAsync(request);
            });

        app.MapPost("api/v1/auth/refresh",
            async (RefreshTokenRequest request) =>
            {
                var result = await new RefreshTokenValidator().ValidateAsync(request);

                if (!result.IsValid)
                {
                    return new RefreshTokenResponse
                        { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
                }

                return await _authClient.GetAccessTokenAsync(request);
            });

        app.MapPost("api/v1/core/categories", async (AddCategoryRequest request) =>
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
        });

        app.MapGet("api/v1/core/categories", async (GetCategoriesRequest request) =>
        {
            var result = await new GetCategoriesValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                return new GetCategoriesResponse
                    { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
            }

            return await _coreClient.GetCategoriesAsync(request);
        });

        app.MapPut("api/v1/core/categories", async (DepositCategoryRequest request) =>
        {
            var result = await new DepositCategoryValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                return new DepositCategoryResponse
                    { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
            }

            return await _coreClient.DepositCategoryAsync(request);
        });

        app.MapDelete("api/v1/core/categories", async (RemoveCategoryRequest request) =>
        {
            var result = await new RemoveCategoryValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                return new RemoveCategoryResponse
                    { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
            }

            return await _coreClient.RemoveCategoryAsync(request);
        });

        app.MapPost("api/v1/core/units", async (AddUnitRequest request) =>
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
        });

        app.MapGet("api/v1/core/units", async (GetUnitsRequest request) =>
        {
            var result = await new GetUnitsValidator().ValidateAsync(request);

            if (!result.IsValid)
            {
                return new GetUnitsResponse
                    { IsSucceed = false, Status = 400, Error = string.Join(", ", result.Errors) };
            }

            return await _coreClient.GetUnitsAsync(request);
        });
    }
}