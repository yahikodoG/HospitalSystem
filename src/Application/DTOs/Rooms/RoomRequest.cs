namespace Application.DTOs.Rooms;

public class RoomRequest
{
    public string RoomName { get; set; } = null!;

    public int StatusId { get; set; }

    public string? Description { get; set; }
}