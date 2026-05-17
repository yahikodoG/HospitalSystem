using System;
using System.Collections.Generic;

namespace Domain.Entities;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string MedicineCode { get; set; } = null!;

    public string MedicineName { get; set; } = null!;

    public int? MedicineTypeId { get; set; }

    public int? MedicineUnitId { get; set; }

    public decimal SellPrice { get; set; }

    public int StockQuantity { get; set; }

    public int? MedicineStatusId { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? CreatedByNavigation { get; set; }

    public virtual User? DeletedByNavigation { get; set; }

    public virtual MedicineStatus? MedicineStatus { get; set; }

    public virtual MedicineType? MedicineType { get; set; }

    public virtual MedicineUnit? MedicineUnit { get; set; }

    public virtual User? UpdatedByNavigation { get; set; }
}
