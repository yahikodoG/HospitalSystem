namespace Application.DTOs.Rooms;

public class RoomResponse
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int? StatusId { get; set; }

    public string? Description { get; set; }
}