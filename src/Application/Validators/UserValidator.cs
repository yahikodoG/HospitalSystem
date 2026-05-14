using Application.DTOs.Users;
using FluentValidation;

namespace Application.Validators;

public class UserValidator : AbstractValidator<UserRequest>
{
    public UserValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Tên đăng nhập không được để trống.")
            .MaximumLength(50).WithMessage("Tên đăng nhập không được vượt quá 50 ký tự.");

        RuleFor(x => x.PasswordHash)
            .NotEmpty().WithMessage("Mật khẩu không được để trống.")
            .MaximumLength(256).WithMessage("Mật khẩu không được vượt quá 256 ký tự.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Họ tên không được để trống.")
            .MaximumLength(100).WithMessage("Họ tên không được vượt quá 100 ký tự.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email không được để trống.")
            .MaximumLength(100).WithMessage("Email không được vượt quá 255 ký tự.")
            .EmailAddress().WithMessage("Email không hợp lệ.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Số điện thoại không được để trống.")
            .MaximumLength(10).WithMessage("Số điện thoại không được vượt quá 10 ký tự.")
            .Matches(@"^0\d{9,10}$").WithMessage("Số điện thoại không hợp lệ.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Địa chỉ không được để trống.")
            .MaximumLength(200).WithMessage("Địa chỉ không được vượt quá 200 ký tự.");
    }
}