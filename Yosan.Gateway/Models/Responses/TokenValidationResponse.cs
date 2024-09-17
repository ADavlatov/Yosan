namespace Yosan.Gateway.Models.Responses;

public class TokenValidationResponse
{
    public bool IsSucceed { get; set; }
    public string Error { get; set; }
    public int Status { get; set; }
}