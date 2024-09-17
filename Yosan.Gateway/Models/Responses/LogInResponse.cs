namespace Yosan.Gateway.Models.Responses;

public class LogInResponse
{
    public bool IsSucceed { get; set; }
    public string UserId { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public string UsernameErrors { get; set; }
    public string PasswordErrors { get; set; }
    public int Status { get; set; }
}