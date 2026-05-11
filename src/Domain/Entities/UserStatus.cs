using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class UserStatus
{
    public int StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
