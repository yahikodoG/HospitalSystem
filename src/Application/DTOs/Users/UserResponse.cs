namespace Application.DTOs.Users;
public class UserResponse
{
    public string Username { get; set; } = null!;


    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int? GenderId { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }
}