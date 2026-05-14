using Application.Common.Responses;
using Application.DTOs.Suppliers;

namespace Application.Interfaces.Services;

public interface ISupplierService
{
    Task<List<SupplierResponse>> GetAllAsync();
    Task<ResponseValue<SupplierResponse?>> GetByIdAsync(int id);
    Task<ResponseValue<SupplierResponse>> CreateAsync(SupplierRequest request);
    Task<ResponseValue<SupplierResponse>> UpdateAsync(SupplierRequest request, int id);
    Task<ResponseValue<bool>> DeleteAsync(int id);
}