using Application.DTOs.Services;
using FluentValidation;

namespace Application.Validators;

public class ServiceValidator : AbstractValidator<ServiceRequest>
{
    public ServiceValidator()
    {
        RuleFor(x => x.ServiceCode)
           .NotEmpty().WithMessage("Mã dịch vụ không được để trống.")
           .MaximumLength(20).WithMessage("Mã dịch vụ không được vượt quá 20 ký tự.");

        RuleFor(x => x.ServiceName)
            .NotEmpty().WithMessage("Tên dịch vụ không được để trống.")
            .MaximumLength(150).WithMessage("Tên dịch vụ không được vượt quá 150 ký tự.");

        RuleFor(x => x.ServiceTypeId)
            .NotNull().WithMessage("Loại dịch vụ không được phép để trống.")
            .GreaterThan(0).WithMessage("Loại dịch vụ không hợp lệ.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Giá tiền không hợp lệ.")
            .LessThanOrEqualTo(100000000000).WithMessage("Giá tiền vượt quá mức cho phép.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Mô tả đã vượt quá 500 ký tự.");
    }
}