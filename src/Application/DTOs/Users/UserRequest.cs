namespace Application.DTOs.Users;

public class UserRequest
{
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public int? GenderId { get; set; }

    public string? Address { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public int? StatusId { get; set; }

    public bool MustChangePassword { get; set; }

    public bool MustUpdateAccount { get; set; }
}