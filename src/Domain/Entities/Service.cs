using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Service
{
    public int ServiceId { get; set; }

    public string ServiceCode { get; set; } = null!;

    public string ServiceName { get; set; } = null!;

    public int? ServiceTypeId { get; set; }

    public decimal Price { get; set; }

    public string? Description { get; set; }

    public virtual ServiceType? ServiceType { get; set; }
}
