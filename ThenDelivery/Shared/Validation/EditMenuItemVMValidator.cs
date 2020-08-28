using FluentValidation;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
   public class EditMerchantValidator : AbstractValidator<EditMerchantVM>
   {
      public EditMerchantValidator()
      {
         RuleForEach(e => e.MenuItemList).SetValidator(new EditMenuItemVMValidator());
      }
   }

   public class EditMenuItemVMValidator : AbstractValidator<EditMenuItemVM>
   {
      public EditMenuItemVMValidator()
      {
         RuleFor(e => e.Name).NotNull()
            .When(e => string.IsNullOrWhiteSpace(e.Name)).WithMessage("Vui lòng nhập tên nhóm")
            .NotNull().When(e => e.ProductList is null || e.ProductList.Count == 0)
            .WithMessage("Bạn phải có ít nhất 1 món trong thực đơn");
         RuleFor(e => e.ProductList).NotNull()
            .NotNull().When(e => e.ProductList == null || e.ProductList.Count == 0)
            .WithMessage("Bạn phải có ít nhất 1 món trong thực đơn");
         RuleForEach(e => e.ProductList).SetValidator(new EditProductVMValidator());
      }
   }

   public class EditProductVMValidator : AbstractValidator<EditProductVM>
   {
      public EditProductVMValidator()
      {
         RuleFor(e => e.Image).NotNull().WithMessage("Vui lòng chọn ảnh đại diện cho món ăn");
         RuleFor(e => e.Name).NotNull()
            .When(e => string.IsNullOrWhiteSpace(e.Name)).WithMessage("Vui lòng nhập tên cho món ăn");
         RuleFor(e => e.UnitPrice).GreaterThanOrEqualTo(Const.MinProductUnitPrice)
            .WithMessage($"Giá bán phải lớn hơn hoặc bằng {Const.MinProductUnitPrice:N0}");
         RuleForEach(e => e.ToppingList).SetValidator(new EditToppingVMValidator());
      }
   }

   public class EditToppingVMValidator : AbstractValidator<ToppingDto>
   {
      public EditToppingVMValidator()
      {
         RuleFor(e => e.Name).NotNull()
            .When(e => string.IsNullOrWhiteSpace(e.Name)).WithMessage("Vui lòng nhập tên cho topping");
         RuleFor(e => e.UnitPrice).GreaterThanOrEqualTo(Const.MinToppingUnitPrice)
            .WithMessage($"Giá bán phải lớn hơn hoặc bằng {Const.MinToppingUnitPrice:N0}");
      }
   }
}
