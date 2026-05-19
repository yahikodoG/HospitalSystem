using Application.DTOs.Medicines;
using Domain.Entities;

namespace Application.Mappings;

public static class MedicineMapping
{
    public static MedicineResponse MapToResponse(this Medicine medicine)
    {
        return new MedicineResponse
        {
            MedicineCode = medicine.MedicineCode,
            MedicineName = medicine.MedicineName,
            MedicineTypeId = medicine.MedicineTypeId,
            MedicineUnitId = medicine.MedicineUnitId,
            SellPrice = medicine.SellPrice,
            StockQuantity = medicine.StockQuantity,
            MedicineStatusId = medicine.MedicineStatusId,
            Description = medicine.Description
        };
    }
}