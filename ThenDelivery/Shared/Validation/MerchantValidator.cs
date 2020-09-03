using FluentValidation;
using ThenDelivery.Shared.Common;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Shared.Validation
{
	public class MerchantValidator : AbstractValidator<MerchantDto>
	{
		public MerchantValidator()
		{
			RuleFor(e => e.Avatar).NotEmpty().WithMessage("Vui lòng chọn avatar");
			RuleFor(e => e.CoverPicture).NotEmpty().WithMessage("Vui lòng chọn cover picture");
			RuleFor(e => e.OpenTime.TimeString).NotEmpty().WithMessage("Vui lòng nhập giờ mở quán");
			RuleFor(e => e.OpenTime.TimeString).NotEmpty()
				.When(t => t.OpenTime.Hour > 23 || t.OpenTime.Minute > 59).WithMessage("Giờ phút không hợp lệ");
			RuleFor(e => e.CloseTime.TimeString).NotEmpty().WithMessage("Vui lòng nhập giờ đóng quán");
			RuleFor(e => e.CloseTime.TimeString).NotEmpty()
				.When(t => t.CloseTime.Hour > 23 || t.CloseTime.Minute > 59).WithMessage("Giờ phút không hợp lệ");

			RuleFor(e => e.HouseNumber).NotEmpty().WithMessage("Vui lòng nhập địa chỉ quán");
			RuleFor(e => e.City).NotEmpty().WithMessage("Vui lòng chọn tỉnh thành / thành phố");
			RuleFor(e => e.District).NotEmpty().WithMessage("Vui lòng chọn thành phố / quận / huyện");
			RuleFor(e => e.Ward).NotEmpty().WithMessage("Vui lòng chọn phường / xã / thị trấn");
			RuleFor(e => e.Name).NotEmpty().WithMessage("Vui lòng nhập tên quán");
			RuleFor(e => e.PhoneNumber).NotEmpty().WithMessage("Vui lòng nhập số điện thoại")
				.Length(10).WithMessage("Số điện thoại phải đủ 10 số");
			RuleFor(e => e.TaxCode).NotEmpty().WithMessage("Vui lòng nhập mã số thuế")
				.Length(10).WithMessage("Mã số thuế phải đủ 10 số");
			RuleFor(e => e.SearchKey).NotEmpty().WithMessage("Vui lòng nhập từ khóa tìm kiếm")
				.MaximumLength(20).WithMessage("Từ khóa tìm kiếm tối đa 20 ký tự");
			RuleFor(e => e.MerTypeList).NotEmpty().WithMessage("Vui lòng chọn loại quán")
				.When(t => t.MerTypeList.Count > Const.MaxMerTypePerMerchant)
				.WithMessage("Loại quán tối đa là 2");
			RuleFor(e => e.FeaturedDishList).NotEmpty().WithMessage("Vui lòng chọn món đặc trưng cho quán")
				.When(t => t.MerTypeList.Count > Const.MaxFeaturedDishPerMerchant)
				.WithMessage("Số lượng món đặc trưng tối đa là 3");
		}
	}
}
