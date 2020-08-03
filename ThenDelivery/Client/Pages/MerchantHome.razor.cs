using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Pages
{
	public class MerchantHomeBase : CustomComponentBase<MerchantHomeBase>
	{
		[Parameter] public int MerchantId { get; set; }
		public MerchantDto Merchant { get; set; }

		protected override async Task OnInitializedAsync()
		{
			Merchant = await HttpClientAnonymous.CustomGetAsync<MerchantDto>("api/merchant");
		}
	}
}
