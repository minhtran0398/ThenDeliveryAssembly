using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using System.Linq;

namespace ThenDelivery.Client.Pages
{
	public class HomeBase : CustomComponentBase<HomeBase>
	{
		public List<MerchantDto> MerchantList { get; set; }
		public List<FeaturedDishDto> FeaturedDishList { get; set; }

		protected override async Task OnInitializedAsync()
		{
			MerchantList = await HttpClientAnonymous.CustomGetAsync<List<MerchantDto>>("api/merchant");
			FeaturedDishList = (await HttpClientAnonymous.CustomGetAsync<List<FeaturedDishDto>>("api/featureddish")).Take(10).ToList();
		}
	}
}
