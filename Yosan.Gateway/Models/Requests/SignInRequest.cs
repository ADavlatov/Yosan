namespace Yosan.Gateway.Models.Requests;

public class SignInRequest
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}