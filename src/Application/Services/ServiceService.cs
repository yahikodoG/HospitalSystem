using Application.Common.Errors;
using Application.Common.Responses;
using Application.DTOs.Services;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class ServiceService : IServiceService
{
    private readonly IServiceRepository _serviceRepository;
    private readonly IUnitOfWork _uow;
    private readonly IValidator<ServiceRequest> _validator;

    public ServiceService(
        IServiceRepository serviceRepository,
        IUnitOfWork uow,
        IValidator<ServiceRequest> validator)
    {
        _serviceRepository = serviceRepository;
        _uow = uow;
        _validator = validator;
    }

    public async Task<List<ServiceResponse>> GetAllAsync()
    {
        var serivces = await _serviceRepository.GetAllAsync();
        return serivces.Select(s => s.MapToResponse()).ToList();
    }

    public async Task<ResponseValue<ServiceResponse?>> GetByIdAsync(int id)
    {
        var serivce = await _serviceRepository.GetByIdAsync(id);

        if (serivce == null)
            return ResponseValue<ServiceResponse?>.NotFound(ServiceErrors.ERR_NOT_FOUND);

        return ResponseValue<ServiceResponse?>.Success(
            serivce.MapToResponse(),
            "Lấy dịch vụ thành công."
        );
    }

    public async Task<ResponseValue<ServiceResponse>> CreateAsync(ServiceRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<ServiceResponse>.BadRequest(errors);
        }

        var service = new Service
        {
            ServiceCode = request.ServiceCode,
            ServiceName = request.ServiceName,
            ServiceTypeId = request.ServiceTypeId,
            Price = request.Price,
            Description = request.Description
        };

        await _serviceRepository.AddAsync(service);
        await _uow.SaveChangesAsync();

        return ResponseValue<ServiceResponse>.Success(
        service.MapToResponse(),
        "Tạo dịch vụ thành công."
        );
    }

    public async Task<ResponseValue<ServiceResponse>> UpdateAsync(ServiceRequest request, int id)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<ServiceResponse>.BadRequest(errors);
        }
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
            return ResponseValue<ServiceResponse>.NotFound(ServiceErrors.ERR_NOT_FOUND);

        service.ServiceCode = request.ServiceCode;
        service.ServiceName = request.ServiceName;
        service.ServiceTypeId = request.ServiceTypeId;
        service.Price = request.Price;
        service.Description = request.Description;

        _serviceRepository.Update(service);
        await _uow.SaveChangesAsync();

        return ResponseValue<ServiceResponse>.Success(
            service.MapToResponse(),
            "Cập nhật dịch vụ thành công."
        );
    }

    public async Task<ResponseValue<bool>> DeleteAsync(int id)
    {
        var service = await _serviceRepository.GetByIdAsync(id);

        if (service == null)
            return ResponseValue<bool>.NotFound(ServiceErrors.ERR_NOT_FOUND);

        _serviceRepository.Update(service);
        await _uow.SaveChangesAsync();

        return ResponseValue<bool>.Success(true, "Xóa dịch vụ thành công.");
    }
}