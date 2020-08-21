using ThenDelivery.Client.Components;
using System.Collections.Generic;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Client.ExtensionMethods;
using System.Threading.Tasks;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace ThenDelivery.Client.Pages
{
	public class ShipperOrderListBase : CustomComponentBase<ShipperOrderListBase>
	{
		public List<OrderDto> OrderList { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			OrderList = await HttpClientServer
				.CustomGetAsync<List<OrderDto>>($"api/Order?orderStatus={(byte)OrderStatus.ShipperAccept}&isShipper={true}");
		}

		protected async Task HandleOnChangeOrderStatus(OrderDto order)
		{
			Logger.LogInformation("HandleOnChangeOrderStatus");
			order.Status = OrderStatus.Delivery;
			var responseModel =
				await HttpClientServer.CustomPutAsync<OrderDto, CustomResponse>("api/Order", order);
			if (responseModel.IsSuccess == false)
			{
				order.Status = OrderStatus.ShipperAccept;
			}
			await InvokeAsync(StateHasChanged);
		}
	}
}