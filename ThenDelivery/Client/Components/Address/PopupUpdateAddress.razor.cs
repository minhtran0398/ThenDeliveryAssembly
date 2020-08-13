using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Address
{
	public class PopupUpdateAddressBase : CustomComponentBase<PopupUpdateAddressBase>
	{
		public DateTime DeleveryDateTime { get; set; }

		protected List<ShippingAddressDto> ShippingAddressList { get; set; }

		protected override async Task OnInitializedAsync()
		{
			ShippingAddressList =
				await HttpClientServer.CustomGetAsync<List<ShippingAddressDto>>("api/ShippingAddress?userId=bc59a6fe-b41a-4f54-bb0f-5bb9664e1785");
		}

		protected void HanldeEditShippingAddress()
		{

		}
	}
}
