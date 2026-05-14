using Application.DTOs.Rooms;
using FluentValidation;

namespace Application.Validators;

public class RoomValidator : AbstractValidator<RoomRequest>
{
    public RoomValidator()
    {
        RuleFor(x => x.RoomName)
            .NotEmpty().WithMessage("Tên phòng không được để trống.")
            .MaximumLength(50).WithMessage("Tên phòng không được vượt quá 50 ký tự.");

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage("Mô tả không được vượt quá 200 ký tự.");
    }
}