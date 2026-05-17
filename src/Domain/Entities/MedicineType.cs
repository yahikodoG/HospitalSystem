using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicineType
{
    public int MedicineTypeId { get; set; }

    public string MedicineTypeName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
