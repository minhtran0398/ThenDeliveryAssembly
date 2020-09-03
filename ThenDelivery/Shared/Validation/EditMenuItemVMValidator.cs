using FluentValidation;
using System.Collections.Generic;
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
			RuleFor(e => e.Name).NotEmpty().WithMessage("Vui lòng nhập tên nhóm")
				.WithMessage("Bạn phải có ít nhất 1 món trong thực đơn");
			RuleFor(e => e.ProductList).NotEmpty()
				.WithMessage("Bạn phải có ít nhất 1 món trong thực đơn");
			//RuleForEach(e => e.ProductList).SetValidator(new EditProductVMValidator());
		}
	}

	public class EditProductVMValidator : AbstractValidator<EditProductVM>
	{
		public EditProductVMValidator()
		{
			RuleFor(e => e.Image).NotEmpty().WithMessage("Vui lòng chọn ảnh đại diện cho món ăn");
			RuleFor(e => e.Name).NotEmpty().WithMessage("Vui lòng nhập tên cho món ăn");
			RuleFor(e => e.UnitPrice).GreaterThanOrEqualTo(Const.MinProductUnitPrice)
				.WithMessage($"Giá bán phải lớn hơn hoặc bằng {Const.MinProductUnitPrice:N0}");
			RuleForEach(e => e.ToppingList).SetValidator(new EditToppingVMValidator());
		}
	}

	public class Temp
	{
		public List<ToppingDto> ToppingDtoList { get; set; }
	}


	public class TempValidator : AbstractValidator<Temp>
	{
		public TempValidator()
		{
			RuleForEach(e => e.ToppingDtoList).SetValidator(new EditToppingVMValidator());
		}
	}

	public class EditToppingVMValidator : AbstractValidator<ToppingDto>
	{
		public EditToppingVMValidator()
		{
			RuleFor(e => e.Name).NotNull().WithMessage("Vui lòng nhập tên cho món ăn kèm");
			RuleFor(e => e.UnitPrice).GreaterThanOrEqualTo(Const.MinToppingUnitPrice)
				.WithMessage($"Giá bán món ăn kèm phải lớn hơn hoặc bằng {Const.MinToppingUnitPrice:N0}");
		}
	}
}
