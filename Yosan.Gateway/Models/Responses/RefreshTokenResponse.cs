namespace Yosan.Gateway.Models.Responses;

public class RefreshTokenResponse
{
    public bool IsSucceed { get; set; }
    public string UserId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string Error { get; set; }
    public int Status { get; set; }
}