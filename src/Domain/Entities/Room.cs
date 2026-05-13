namespace Domain.Entities;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int? StatusId { get; set; }

    public string? Description { get; set; }

    public virtual RoomStatus? Status { get; set; }
}
