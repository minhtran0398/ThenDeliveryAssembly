using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Components
{
	public class OrderListCpnBase : CustomComponentBase<OrderListCpnBase>
	{
		[Parameter] public List<OrderDto> OrderList { get; set; }
		public CustomResponse ResponseModel { get; set; }
		public int Count { get; set; }
		public int CancelOrderId { get; set; }
		public bool IsShowPopup { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			Count = 0;
		}

		protected override void OnAfterRender(bool firstRender)
		{
			Count = 0;
		}

		protected void HandleCancelOrder(OrderDto order)
		{
			IsShowPopup = true;
			CancelOrderId = order.Id;
		}

		protected async Task CancelOrder()
		{
			IsShowPopup = false;
			var order = OrderList.SingleOrDefault(e => e.Id == CancelOrderId);
			if (order != null)
			{
				ResponseModel = await HttpClientServer.CustomPutAsync("api/order/cancel", order);
				if (ResponseModel.IsSuccess)
				{
					order.Status = OrderStatus.Cancel;
				}
				else
				{
					OrderList = await HttpClientServer
						.CustomGetAsync<List<OrderDto>>($"api/Order/user-order-history/{(byte)OrderStatus.None}");
				}
				ResponseModel.IsShowPopup = true;
			}
			await InvokeAsync(StateHasChanged);
		}
	}
}