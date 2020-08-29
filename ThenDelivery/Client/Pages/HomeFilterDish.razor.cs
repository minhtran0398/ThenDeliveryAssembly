using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Pages
{
	public class HomeFilterDishBase : CustomComponentBase<HomeFilterDishBase>
	{
		[Parameter] public int FeaturedDishId { get; set; }
		public List<MerchantDto> MerchantListFull { get; set; }
		public List<MerchantDto> MerchantList { get; set; }
		public FeaturedDishDto FeaturedDish { get; set; }

		protected override async Task OnInitializedAsync()
		{
			FeaturedDish =
				await HttpClientAnonymous.CustomGetAsync<FeaturedDishDto>($"api/featureddish?id={FeaturedDishId}");
			MerchantListFull =
				await HttpClientAnonymous.CustomGetAsync<List<MerchantDto>>("api/merchant");
			if (FeaturedDish != null)
			{
				MerchantList = MerchantListFull
					.Where(e => e.FeaturedDishList.Contains(FeaturedDish, new FeaturedDishComparerId())).ToList();
			}
		}
	}
}