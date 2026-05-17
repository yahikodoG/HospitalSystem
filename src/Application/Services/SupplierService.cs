using System.Dynamic;
using Application.Common.Errors;
using Application.Common.Responses;
using Application.DTOs.Suppliers;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<SupplierRequest> _validator;

    public SupplierService(
        ISupplierRepository supplierRepository,
        IUnitOfWork uow,
        IValidator<SupplierRequest> validator)
    {
        _supplierRepository = supplierRepository;
        _uow = uow;
        _validator = validator;
    }

    public async Task<List<SupplierResponse>> GetAllAsync()
    {
        var suppliers = await _supplierRepository.GetAllAsync();
        return suppliers.Select(s => s.MapToResponse()).ToList();
    }

    public async Task<ResponseValue<SupplierResponse?>> GetByIdAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier == null)
            return ResponseValue<SupplierResponse?>.NotFound(SupplierErrors.ERR_NOT_FOUND);

        return ResponseValue<SupplierResponse?>.Success(
            supplier.MapToResponse(),
            "Lấy nhà cung cấp thành công."
        );
    }

    public async Task<ResponseValue<SupplierResponse>> CreateAsync(SupplierRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<SupplierResponse>.BadRequest(errors);
        }
        var supplier = new Supplier
        {
            SupplierCode = request.SupplierCode,
            SupplierName = request.SupplierName,
            ContactEmail = request.ContactEmail,
            ContactPhone = request.ContactPhone,
            Address = request.Address,
            Description = request.Description
        };

        await _supplierRepository.AddAsync(supplier);
        await _uow.SaveChangesAsync();

        return ResponseValue<SupplierResponse>.Success(
            supplier.MapToResponse(),
            "Tạo nhà cung cấp thành công."
        );
    }

    public async Task<ResponseValue<SupplierResponse>> UpdateAsync(SupplierRequest request, int id)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<SupplierResponse>.BadRequest(errors);
        }
        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier == null)
            return ResponseValue<SupplierResponse>.NotFound(SupplierErrors.ERR_NOT_FOUND);

        supplier.SupplierCode = request.SupplierCode;
        supplier.SupplierName = request.SupplierName;
        supplier.ContactEmail = request.ContactEmail;
        supplier.ContactPhone = request.ContactPhone;
        supplier.Address = request.Address;
        supplier.Description = request.Description;

        _supplierRepository.Update(supplier);
        await _uow.SaveChangesAsync();

        return ResponseValue<SupplierResponse>.Success(
            supplier.MapToResponse(),
            "Cập nhật nhà cung cấp thành công."
        );
    }

    public async Task<ResponseValue<bool>> DeleteAsync(int id)
    {
        var supplier = await _supplierRepository.GetByIdAsync(id);

        if (supplier == null)
            return ResponseValue<bool>.NotFound(SupplierErrors.ERR_NOT_FOUND);

        _supplierRepository.Update(supplier);
        await _uow.SaveChangesAsync();

        return ResponseValue<bool>.Success(true, "Xóa nhà cung cấp thành công.");
    }
}