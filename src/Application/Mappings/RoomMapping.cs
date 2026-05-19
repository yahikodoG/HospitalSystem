using Application.DTOs.Rooms;
using Domain.Entities;

namespace Application.Mappings;

public static class RoomMapping
{
    public static RoomResponse MapToResponse(this Room room)
    {
        return new RoomResponse
        {
            RoomId = room.RoomId,
            RoomName = room.RoomName,
            StatusId = room.StatusId,
            Description = room.Description
        };
    }

    public static Room MapToEntity(this RoomRequest request)
    {
        return new Room
        {
            RoomName = request.RoomName,
            StatusId = request.StatusId,
            Description = request.Description
        };
    }
}