namespace Application.DTOs.Medicines;

public class MedicineRequest
{
    public string MedicineCode { get; set; } = null!;

    public string MedicineName { get; set; } = null!;

    public int? MedicineTypeId { get; set; }

    public int? MedicineUnitId { get; set; }

    public decimal SellPrice { get; set; }

    public int StockQuantity { get; set; }

    public int? MedicineStatusId { get; set; }

    public string? Description { get; set; }
}