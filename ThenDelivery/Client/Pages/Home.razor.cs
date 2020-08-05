﻿using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Pages
{
	public class HomeBase : CustomComponentBase<HomeBase>
	{
		public List<MerchantDto> MerchantList { get; set; }

		protected override async Task OnInitializedAsync()
		{
			MerchantList = await HttpClientAnonymous.CustomGetAsync<List<MerchantDto>>("api/merchant");
		}
	}
}
