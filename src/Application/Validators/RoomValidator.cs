using Application.Common.Errors;
using Application.DTOs.Rooms;
using FluentValidation;

namespace Application.Validators;

public class RoomValidator : AbstractValidator<RoomRequest>
{
    public RoomValidator()
    {
        RuleFor(x => x.RoomName)
            .NotEmpty().WithMessage(RoomErrors.ERR_NAME_EMPTY)
            .MaximumLength(50).WithMessage(RoomErrors.ERR_NAME_LENGTH);

        RuleFor(x => x.Description)
            .MaximumLength(200).WithMessage(RoomErrors.ERR_DESC_LENGTH);
    }
}