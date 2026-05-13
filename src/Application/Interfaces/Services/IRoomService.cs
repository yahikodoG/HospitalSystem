using Application.Common.Responses;
using Application.DTOs.Rooms;

namespace Application.Interfaces.Services;

public interface IRoomService
{
    Task<List<RoomResponse>> GetAllAsync();
    Task<ResponseValue<RoomResponse?>> GetByIdAsync(int id);
    Task<ResponseValue<RoomResponse>> CreateAsync(RoomRequest request);
    Task<ResponseValue<RoomResponse>> UpdateAsync(RoomRequest request, int id);
    Task<ResponseValue<bool>> DeleteAsync(int id);
}