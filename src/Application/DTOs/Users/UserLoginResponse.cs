namespace Application.DTOs.Users;

public class UserLoginResponse
{
    public string? AccessToken { get; set; }
    public string? Username { get; set; }
    public string FullName { get; set; }
    public string? Role { get; set; }
}