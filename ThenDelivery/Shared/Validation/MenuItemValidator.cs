using FluentValidation;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
	public class MenuItemValidator : AbstractValidator<MenuItemDto>
	{
		public MenuItemValidator()
		{
			RuleFor(e => e.Name).NotEmpty().WithMessage("Vui lòng nhập tên thực đơn");
		}
	}
}
