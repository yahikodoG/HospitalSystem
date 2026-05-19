using Application.Common.Errors;
using Application.Common.Responses;
using Application.DTOs.Medicines;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class MedicineService : IMedicineService
{
    private readonly IMedicineRepository _medicineRepository;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<MedicineRequest> _validator;

    public MedicineService(
        IMedicineRepository medicineRepository,
        IUnitOfWork uow,
        IValidator<MedicineRequest> validator)
    {
        _medicineRepository = medicineRepository;
        _uow = uow;
        _validator = validator;
    }

    public async Task<List<MedicineResponse>> GetAllAsync()
    {
        var medicines = await _medicineRepository.GetAllAsync();
        return medicines.Select(m => m.MapToResponse()).ToList();
    }

    public async Task<ResponseValue<MedicineResponse?>> GetByIdAsync(int id)
    {
        var medicine = await _medicineRepository.GetByIdAsync(id);

        if (medicine == null)
            return ResponseValue<MedicineResponse?>.NotFound(MedicineErrors.ERR_NOT_FOUND);

        return ResponseValue<MedicineResponse?>.Success(
            medicine.MapToResponse(),
            "Lấy thuốc thành công."
        );
    }

    public async Task<ResponseValue<MedicineResponse>> CreateAsync(MedicineRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<MedicineResponse>.BadRequest(errors);
        }

        var medicine = new Medicine
        {
            MedicineCode = request.MedicineCode,
            MedicineName = request.MedicineName,
            MedicineTypeId = request.MedicineTypeId,
            MedicineUnitId = request.MedicineUnitId,
            SellPrice = request.SellPrice,
            StockQuantity = request.StockQuantity,
            MedicineStatusId = request.MedicineStatusId,
            Description = request.Description
        };

        await _medicineRepository.AddAsync(medicine);
        await _uow.SaveChangesAsync();

        return ResponseValue<MedicineResponse>.Success(
            medicine.MapToResponse(),
            "Tạo thuốc thành công."
        );
    }

    public async Task<ResponseValue<MedicineResponse>> UpdateAsync(MedicineRequest request, int id)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<MedicineResponse>.BadRequest(errors);
        }

        var medicine = await _medicineRepository.GetByIdAsync(id);

        if (medicine == null)
            return ResponseValue<MedicineResponse>.NotFound(MedicineErrors.ERR_NOT_FOUND);

        medicine.MedicineCode = request.MedicineCode;
        medicine.MedicineName = request.MedicineName;
        medicine.MedicineTypeId = request.MedicineTypeId;
        medicine.MedicineUnitId = request.MedicineUnitId;
        medicine.SellPrice = request.SellPrice;
        medicine.StockQuantity = request.StockQuantity;
        medicine.MedicineStatusId = request.MedicineStatusId;
        medicine.Description = request.Description;

        _medicineRepository.Update(medicine);
        await _uow.SaveChangesAsync();

        return ResponseValue<MedicineResponse>.Success(
            medicine.MapToResponse(),
            "Cập nhật thuốc thành công."
        );
    }

    public async Task<ResponseValue<bool>> DeleteAsync(int id)
    {
        var medicine = await _medicineRepository.GetByIdAsync(id);

        if (medicine == null)
            return ResponseValue<bool>.NotFound(MedicineErrors.ERR_NOT_FOUND);

        _medicineRepository.Update(medicine);
        await _uow.SaveChangesAsync();

        return ResponseValue<bool>.Success(true, "Xóa thuốc thành công.");
    }
}