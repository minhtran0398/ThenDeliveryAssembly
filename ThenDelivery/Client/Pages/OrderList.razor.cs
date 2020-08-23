using ThenDelivery.Client.Components;
using Microsoft.AspNetCore.Authorization;
using ThenDelivery.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Pages
{
	[Authorize]
	public class OrderListBase : CustomComponentBase<OrderListBase>
	{
		public List<OrderDto> OrderList { get; set; }
		public bool IsShowPopupConfirm { get; set; }
		public bool IsShowPopupResult { get; set; }
		public OrderDto SelectedOrder { get; set; }
		public CustomResponse ResponseModel { get; set; }

		protected override async Task OnInitializedAsync()
		{
			await InvokeAsync(StateHasChanged);
			await LoadOrderList();
			Logger.LogInformation(JsonConvert.SerializeObject(OrderList));
		}

		protected async Task HandleShowChooseOrder(OrderDto order)
		{
			IsShowPopupConfirm = true;
			SelectedOrder = order;
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleClose()
		{
			IsShowPopupConfirm = false;
		}

		protected async Task HandleConfirm()
		{
			IsShowPopupConfirm = false;
			SelectedOrder.Status = OrderStatus.ShipperAccept;
			ResponseModel =
				await HttpClientServer.CustomPutAsync<OrderDto, CustomResponse>("api/Order", SelectedOrder);
			if (ResponseModel.IsSuccess == false)
			{
				await LoadOrderList();
			}
			else
			{
				OrderList.RemoveFirst(e => e.Id == SelectedOrder.Id);
			}
			IsShowPopupResult = true;
		}

		protected async Task LoadOrderList()
		{
			OrderList =
				await HttpClientServer.CustomGetAsync<List<OrderDto>>($"api/Order?orderStatus={(byte)OrderStatus.OrderSuccess}");
		}
	}
}