namespace Domain.Common;
public class BaseEntity
{
    public DateTime? DeletedAt { get; set; }

    public int? DeletedBy { get; set; }
}
