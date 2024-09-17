namespace Yosan.Gateway.Models.Requests;

public class TokenValidationRequest
{
    public string UserId { get; set; }
    public string AccessToken { get; set; }
}