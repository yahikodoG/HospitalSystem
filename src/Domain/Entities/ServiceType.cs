using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class ServiceType
{
    public int ServiceTypeId { get; set; }

    public string ServiceTypeName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
