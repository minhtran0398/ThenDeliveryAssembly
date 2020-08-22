using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;

namespace ThenDelivery.Client.Pages
{
	public class OrderHistoryBase : CustomComponentBase<OrderHistoryBase>
	{
		public List<OrderDto> OrderList { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			OrderList = await HttpClientServer
				.CustomGetAsync<List<OrderDto>>($"api/Order/user-order-history/{(byte)OrderStatus.None}");
		}
	}
}