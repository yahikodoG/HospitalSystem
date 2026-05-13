using Application.Common.Errors;
using Application.Common.Responses;
using Application.DTOs.Rooms;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappings;
using Domain.Entities;
using FluentValidation;

namespace Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<RoomRequest> _validator;
    private readonly IUnitOfWork _uow;

    public RoomService(
        IRoomRepository roomRepository,
        IValidator<RoomRequest> validator,
        IUnitOfWork uow)
    {
        _roomRepository = roomRepository;
        _validator = validator;
        _uow = uow;
    }

    public async Task<List<RoomResponse>> GetAllAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms.Select(r => r.MapToResponse()).ToList();
    }

    public async Task<ResponseValue<RoomResponse?>> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);

        if (room == null)
            return ResponseValue<RoomResponse?>.NotFound(RoomErrors.ERR_NOT_FOUND);

        return ResponseValue<RoomResponse?>.Success(
            room.MapToResponse(),
            "Lấy phòng thành công."
        );
    }

    public async Task<ResponseValue<RoomResponse>> CreateAsync(RoomRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<RoomResponse>.BadRequest(errors);
        }

        var room = new Room
        {
            RoomName = request.RoomName,
            StatusId = request.StatusId,
            Description = request.Description
        };

        await _roomRepository.AddAsync(room);
        await _uow.SaveChangesAsync();

        return ResponseValue<RoomResponse>.Success(
            room.MapToResponse(),
            "Tạo phòng thành công."
        );
    }

    public async Task<ResponseValue<RoomResponse>> UpdateAsync(RoomRequest request, int id)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if(!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<RoomResponse>.BadRequest(errors);
        }

        var room = await _roomRepository.GetByIdAsync(id);

        if (room == null)
            return ResponseValue<RoomResponse>.NotFound(RoomErrors.ERR_NOT_FOUND);

        room.RoomName = request.RoomName;
        room.StatusId = request.StatusId;
        room.Description = request.Description;

        _roomRepository.Update(room);
        await _uow.SaveChangesAsync();

        return ResponseValue<RoomResponse>.Success(
            room.MapToResponse(),
            "Cập nhật phòng thành công."
        );
    }

    public async Task<ResponseValue<bool>> DeleteAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);

        if (room == null)
            return ResponseValue<bool>.NotFound(RoomErrors.ERR_NOT_FOUND);

        _roomRepository.Update(room);
        await _uow.SaveChangesAsync();

        return ResponseValue<bool>.Success(true, "Xóa phòng thành công.");
    }
}