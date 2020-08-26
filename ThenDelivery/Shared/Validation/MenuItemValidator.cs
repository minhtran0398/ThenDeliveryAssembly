using FluentValidation;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
   public class MenuItemValidator : AbstractValidator<MenuItemDto>
   {
      public MenuItemValidator()
      {
         RuleFor(e => e.Name).NotNull().WithMessage("Vui lòng nhập tên thực đơn");
      }
   }
}
