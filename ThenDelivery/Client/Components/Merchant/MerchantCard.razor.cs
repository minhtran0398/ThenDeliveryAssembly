using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Merchant
{
	public class MerchantCardBase : ComponentBase
	{
		[Parameter] public MerchantDto MerchantModel { get; set; }

	}
}
