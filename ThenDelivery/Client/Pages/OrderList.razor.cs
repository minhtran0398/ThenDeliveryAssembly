using ThenDelivery.Client.Components;
using Microsoft.AspNetCore.Authorization;
using ThenDelivery.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ThenDelivery.Client.Pages
{
	[Authorize]
	public class OrderListBase : CustomComponentBase<OrderListBase>
	{
		public List<OrderDto> OrderList { get; set; }

		protected override async Task OnInitializedAsync()
		{
			OrderList = await HttpClientServer.CustomGetAsync<List<OrderDto>>("api/Order");
			Logger.LogInformation(JsonConvert.SerializeObject(OrderList));
		}
	}
}