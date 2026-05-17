using Application.DTOs.Suppliers;
using Domain.Entities;

namespace Application.Mappings;

public static class SupplierMappings
{
    public static SupplierResponse MapToResponse(this Supplier supplier)
    {
        return new SupplierResponse
        {
            SupplierId = supplier.SupplierId,
            SupplierCode = supplier.SupplierCode,
            SupplierName = supplier.SupplierName,
            ContactEmail = supplier.ContactEmail,
            ContactPhone = supplier.ContactPhone,
            Address = supplier.Address,
            Description = supplier.Description
        };
    }
}