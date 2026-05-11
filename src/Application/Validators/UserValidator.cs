using Application.Common.Errors;
using Application.DTOs.Users;
using FluentValidation;

namespace Application.Validators;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage(UserErrors.ERR_USERNAME_EMPTY)
            .MaximumLength(50).WithMessage("ERR_USERNAME_LENGTH");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage(UserErrors.ERR_PASSWORD_EMPTY)
            .MaximumLength(256).WithMessage(UserErrors.ERR_PASSWORD_LENGTH);

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage(UserErrors.ERR_FULLNAME_EMPTY)
            .MaximumLength(100).WithMessage(UserErrors.ERR_FULLNAME_LENGTH);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(UserErrors.ERR_EMAIL_EMPTY)
            .MaximumLength(100).WithMessage(UserErrors.ERR_EMAIL_LENGTH)
            .EmailAddress().WithMessage("Email không hợp lệ.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage(UserErrors.ERR_PHONE_EMPTY)
            .MaximumLength(10).WithMessage(UserErrors.ERR_PHONE_LENGTH)
            .Matches(@"^0\d{9,10}$").WithMessage("Số điện thoại không hợp lệ.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage(UserErrors.ERR_ADDRESS_EMPTY)
            .MaximumLength(200).WithMessage(UserErrors.ERR_ADDRESS_LENGTH);
    }
}