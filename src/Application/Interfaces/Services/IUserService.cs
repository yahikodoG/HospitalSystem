using Application.Common.Responses;
using Application.DTOs.Patients;
using Application.DTOs.Users;

namespace Application.Interfaces.Services;

public interface IUserService
{
    Task<List<UserResponse>> GetAllAsync();
    Task<ResponseValue<UserResponse?>> GetByIdAsync(int id);
    Task<ResponseValue<UserResponse>> CreateAsync(PatientRequest request);
    // Task<ResponseValue<UserResponse>> UpdateAsync(int id, PatientRequest request);
    // Task<ResponseValue<bool>> DeleteAsync(int id);
    Task<ResponseValue<UserLoginResponse>> LoginAsync(UserLoginRequest request);
    // Task<ResponseValue<UserResponse>> ResetPasswordAsync(int id);
}