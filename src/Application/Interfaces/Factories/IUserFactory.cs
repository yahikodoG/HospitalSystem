using Application.DTOs.Users;
using Domain.Entities;

namespace Application.Interfaces.Factories;

public interface IUserFactory
{
    Task<User> CreateBaseUser(UserRequest request);
}