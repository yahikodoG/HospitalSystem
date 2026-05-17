using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class MedicineStatus
{
    public int MedicineStatusId { get; set; }

    public string MedicineStatusName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();
}
