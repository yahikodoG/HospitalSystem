using Application.DTOs.Suppliers;
using FluentValidation;

namespace Application.Validators;

public class SupplierValidator : AbstractValidator<SupplierRequest>
{
    public SupplierValidator()
    {
        RuleFor(x => x.SupplierCode)
           .NotEmpty().WithMessage("Mã nhà cung cấp không được để trống.")
           .MaximumLength(20).WithMessage("Mã nhà cung cấp không được vượt quá 20 ký tự.");

        RuleFor(x => x.SupplierName)
            .NotEmpty().WithMessage("Tên nhà cung cấp không được để trống.")
            .MaximumLength(150).WithMessage("Tên nhà cung cấp không được vượt quá 150 ký tự.");

        RuleFor(x => x.ContactEmail)
            .NotEmpty().WithMessage("Email không được phép để trống.")
            .MaximumLength(255).WithMessage("Email không được vượt quá 255 ký tự.")
            .EmailAddress().WithMessage("Email không hợp lệ.");

        RuleFor(x => x.ContactPhone)
            .NotEmpty().WithMessage("Số điện thoại không được để trống.")
            .MaximumLength(10).WithMessage("Số điện thoại không được vượt quá 10 ký tự.")
            .Matches(@"^0\d{9,10}$").WithMessage("Số điện thoại không hợp lệ.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Địa chỉ không được để trống.")
            .MaximumLength(500).WithMessage("Địa chi không được vượt quá 500 ký tự.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả đã vượt quá 500 ký tự.");
    }
}
