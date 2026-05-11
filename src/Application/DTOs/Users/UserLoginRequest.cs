namespace Application.DTOs.Users;
public class UserLoginRequest
{
    public string? UsernameOrEmail { get; set; }
    public string? Password { get; set; }
}
