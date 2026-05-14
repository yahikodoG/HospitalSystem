namespace Application.DTOs.Suppliers;

public class SupplierResponse
{
    public int SupplierId { get; set; }

    public string SupplierCode { get; set; } = null!;

    public string SupplierName { get; set; } = null!;

    public string ContactEmail { get; set; } = null!;

    public string ContactPhone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Description { get; set; }
}