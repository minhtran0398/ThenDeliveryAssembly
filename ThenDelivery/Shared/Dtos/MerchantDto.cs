using System.Collections.Generic;
using ThenDelivery.Shared.Helper;

namespace ThenDelivery.Shared.Dtos
{
	public class MerchantDto
	{
		public MerchantDto()
		{
			OpenTime = new CustomTime();
			CloseTime = new CustomTime();
			City = new CityDto();
			District = new DistrictDto();
			Ward = new WardDto();
			MerchantTypeList = new List<MerchantTypeDto>();
			FeaturedDishCategoryList = new List<FeaturedDishCategoryDto>();
			MenuList = new List<StoreMenuDto>();
		}

		public int MerchantId { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public string CoverPicture { get; set; }
		public string TaxCode { get; set; }
		public string PhoneNumber { get; set; }
		public CustomTime OpenTime { get; set; }
		public CustomTime CloseTime { get; set; }
		public string Description { get; set; }
		public string SearchKey { get; set; } // Split by ","
		public CityDto City { get; set; }
		public DistrictDto District { get; set; }
		public WardDto Ward { get; set; }
		public List<FeaturedDishCategoryDto> FeaturedDishCategoryList { get; set; }
		public List<MerchantTypeDto> MerchantTypeList { get; set; }
		public List<StoreMenuDto> MenuList { get; set; }
		public string HouseNumber { get; set; }
	}
}
