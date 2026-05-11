using Application.DTOs.Users;
using Application.Interfaces;
using Application.Interfaces.Factories;
using Domain.Entities;

namespace Application.Factories;

public class UserFactory : IUserFactory
{
    private readonly IPasswordHasher _passwordHasher;

    public UserFactory(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public async Task<User> CreateBaseUser(UserRequest request)
    {
        return new User
        {
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(request.PasswordHash),
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            GenderId = request.GenderId,
            Address = request.Address,
            DateOfBirth = request.DateOfBirth,
            StatusId = request.StatusId,
            MustChangePassword = true
        };
    }

    public async Task<User> UpdateBaseUser(UserRequest request)
    {
        return new User
        {
            Username = request.Username,
            PasswordHash = _passwordHasher.HashPassword(request.PasswordHash),
            FullName = request.FullName,
            Email = request.Email,
            Phone = request.Phone,
            GenderId = request.GenderId,
            Address = request.Address,
            DateOfBirth = request.DateOfBirth,
            StatusId = request.StatusId,
            MustChangePassword = true
        };
    }
}