using Application.DTOs.Medicines;
using FluentValidation;

namespace Application.Validators;

public class MedicineValidator : AbstractValidator<MedicineRequest>
{
    public MedicineValidator()
    {

    }
}