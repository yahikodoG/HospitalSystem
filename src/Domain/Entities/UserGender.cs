namespace Domain.Entities;

public partial class UserGender
{
    public int GenderId { get; set; }

    public string GenderName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
