using FluentValidation;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
   public class EditMerchantVMValidator : AbstractValidator<EditMerchantVM>
   {
      public EditMerchantVMValidator()
      {
         RuleForEach(e => e.MenuItemList).SetValidator(new MenuItemValidator());
         RuleFor(e => e.MenuItemList)
            .NotNull().When(e => e.MenuItemList == null || e.MenuItemList.Count == 0)
            .WithMessage("Bạn phải có ít nhất 1 thực đơn");
      }
   }
}
