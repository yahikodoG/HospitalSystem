using Application.Common.Responses;
using Application.DTOs.Services;

namespace Application.Interfaces.Services;

public interface IServiceService
{
    Task<List<ServiceResponse>> GetAllAsync();
    Task<ResponseValue<ServiceResponse?>> GetByIdAsync(int id);
    Task<ResponseValue<ServiceResponse>> CreateAsync(ServiceRequest request);
    Task<ResponseValue<ServiceResponse>> UpdateAsync(ServiceRequest request, int id);
    Task<ResponseValue<bool>> DeleteAsync(int id);
}