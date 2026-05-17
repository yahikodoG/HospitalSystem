using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicineUnit
{
    public int MedicineUnitId { get; set; }

    public string MedicineUnitName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
