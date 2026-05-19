using Application.Common.Errors;
using Application.Common.Extensions;
using Application.Common.Pagination;
using Application.Common.Responses;
using Application.DTOs.Rooms;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Mappings;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ResponseValue<PagedResult<RoomResponse>>> GetAllAsync(string? search, int page, int pageSize)
    {
        var rooms = _roomRepository
            .GetQueryable()
            .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
            rooms = rooms.Where(r => r.RoomName.Contains(search));

        var result = await rooms
            .Select(s => s.MapToResponse())
            .ToPagedResultAsync(page, pageSize);

        return ResponseValue<PagedResult<RoomResponse>>.Success(result, "Lấy danh sách phòng thành công.");
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

    public async Task<ResponseValue<RoomResponse>> CreateAsync(
        RoomRequest request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
            return ResponseValue<RoomResponse>.BadRequest(errors);
        }

        var isDuplicate = await _roomRepository.ExistsByNameAsync(request.RoomName);

        if (isDuplicate)
        {
            return ResponseValue<RoomResponse>
                .Conflict(RoomErrors.ERR_DUPLICATE);
        }

        var room = request.MapToEntity();

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
        if (!validationResult.IsValid)
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

        // if (await _roomRepository.HasAppointmentsAsync(id))
        //     return ResponseValue<bool>.Conflict(RoomErrors.ERR_IN_USE);

        _roomRepository.Delete(room);
        await _uow.SaveChangesAsync();

        return ResponseValue<bool>.Success(true, "Xóa phòng thành công.");
    }
}