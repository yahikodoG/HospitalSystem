using Application.DTOs.Users;
using Domain.Entities;

namespace Application.Mappings;

public static class UserMapping
{
    public static UserResponse MapToResponse(this User user)
    {
        return new UserResponse
        {
            Username = user.Username,
            FullName = user.FullName,
            Email = user.Email,
            Phone = user.Phone,
            GenderId = user.GenderId,
            Address = user.Address
        };
    }
}