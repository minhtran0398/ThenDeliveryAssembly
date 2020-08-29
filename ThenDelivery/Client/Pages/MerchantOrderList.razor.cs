using System.Linq;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;
using System;

namespace ThenDelivery.Client.Pages
{
	public class MerchantOrderListBase : CustomComponentBase<MerchantOrderList>
	{
		[Parameter] public int MerchantId { get; set; }
		public List<OrderDto> OrderList { get; set; }
		public CustomResponse ResponseModel { get; set; }
		public int Count { get; set; }
		public bool IsShowPopupConfirm { get; set; }
		public int ConfirmOrderId { get; set; }
		public string PopupConfirmMessage { get; set; }
		public Func<Task> ConfirmAction { get; set; }

		protected override void OnAfterRender(bool firstRender)
		{
			Count = 0;
		}

		protected string GetColorCssOrderStatus(OrderStatus status)
		{
			return status switch
			{
				OrderStatus.MerchantAccept => "bg-primary disabled text-white",
				OrderStatus.ShipperAccept => "bg-info disabled text-white",
				OrderStatus.Delivery => "bg-success disabled text-white",
				OrderStatus.DeliverySuccess => "bg-secondary disabled text-white",
				_ => string.Empty,
			};
		}

		protected void HandleConfirmOrder(OrderDto order)
		{
			ConfirmOrderId = order.Id;
			IsShowPopupConfirm = true;
			PopupConfirmMessage = "Bạn chắc chắn xác nhận đơn hàng này?";
			ConfirmAction = ConfirmOrder;
		}

		protected async Task ConfirmOrder()
		{
			IsShowPopupConfirm = false;
			var order = OrderList.SingleOrDefault(e => e.Id == ConfirmOrderId);
			if (order != null)
			{
				ResponseModel = await HttpClientServer.CustomPutAsync("api/order/merchantAccept", order);
				if (ResponseModel.IsSuccess)
				{
					order.Status = OrderStatus.MerchantAccept;
				}
				else
				{
					OrderList = await HttpClientServer
							.CustomGetAsync<List<OrderDto>>($"api/order/order-of-merchant?merchantId={MerchantId}");
				}
				ResponseModel.IsShowPopup = true;
			}
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleCancelOrder(OrderDto order)
		{
			ConfirmOrderId = order.Id;
			IsShowPopupConfirm = true;
			PopupConfirmMessage = "Bạn chắc chắn muốn hủy đơn hàng này?";
			ConfirmAction = CancelOrder;
		}

		protected async Task CancelOrder()
		{
			IsShowPopupConfirm = false;
			var order = OrderList.SingleOrDefault(e => e.Id == ConfirmOrderId);
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
						.CustomGetAsync<List<OrderDto>>($"api/order/order-of-merchant?merchantId={MerchantId}");
				}
				ResponseModel.IsShowPopup = true;
			}
			await InvokeAsync(StateHasChanged);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			OrderList = await HttpClientServer
				.CustomGetAsync<List<OrderDto>>($"api/order/order-of-merchant?merchantId={MerchantId}");
		}
	}
}
