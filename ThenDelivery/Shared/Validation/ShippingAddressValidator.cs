using FluentValidation;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
   public class ShippingAddressValidator : AbstractValidator<ShippingAddressDto>
   {
      public ShippingAddressValidator()
      {
         RuleFor(e => e.City).NotNull().WithMessage("Vui lòng chọn tỉnh / thành phố");
         RuleFor(e => e.District).NotNull().WithMessage("Vui lòng chọn thành phố / quận / huyện");
         RuleFor(e => e.Ward).NotNull().WithMessage("Vui lòng chọn phường / xã");
         RuleFor(e => e.FullName).NotNull().WithMessage("Vui lòng nhập tên");
         RuleFor(e => e.HouseNumber).NotNull().WithMessage("Vui lòng nhập địa chỉ nhà");
         RuleFor(e => e.PhoneNumber).NotNull().WithMessage("Vui lòng nhập số điện thoại")
            .Length(10).WithMessage("Số điện thoại phải đúng 10 số");
      }
   }
}
