using Application.Common.Responses;
using Application.DTOs.Medicines;

namespace Application.Interfaces.Services;

public interface IMedicineService
{
    Task<List<MedicineResponse>> GetAllAsync();
    Task<ResponseValue<MedicineResponse?>> GetByIdAsync(int id);
    Task<ResponseValue<MedicineResponse>> CreateAsync(MedicineRequest request);
    Task<ResponseValue<MedicineResponse>> UpdateAsync(MedicineRequest request, int id);
    Task<ResponseValue<bool>> DeleteAsync(int id);
}