namespace Yosan.Gateway.Models.Requests;

public class RefreshTokenRequest
{
    public string UserId { get; set; }
    public string RefreshToken { get; set; }
}