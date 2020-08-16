using Microsoft.AspNetCore.Components;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Merchant
{
	public class MerchantCardBase : ComponentBase
	{
		[Parameter] public MerchantDto MerchantModel { get; set; }

	}
}
