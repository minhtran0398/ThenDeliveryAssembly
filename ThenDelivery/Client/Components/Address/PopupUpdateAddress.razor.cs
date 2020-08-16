using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Address
{
	public class PopupUpdateAddressBase : CustomComponentBase<PopupUpdateAddressBase>
	{
		[Parameter] public DateTime DeleveryDateTime { get; set; }
		[Parameter] public List<ShippingAddressDto> ShippingAddressList { get; set; }
		[Parameter] public EventCallback OnCLose { get; set; }
		[Parameter] public EventCallback OnConfirm { get; set; }

		protected void HanldeEditShippingAddress()
		{

		}

		protected async Task HandleClose()
		{
			await OnCLose.InvokeAsync(null);
		}

		protected async Task HandleConfirm()
		{
			await OnConfirm.InvokeAsync(null);
		}
	}
}
