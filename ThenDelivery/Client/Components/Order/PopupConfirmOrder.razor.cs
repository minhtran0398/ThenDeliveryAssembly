using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Components.Order
{
	public enum DisplayPopup
	{
		OrderConfirm,
		ChangeAddress
	}

	public class PopupConfirmOrderBase : CustomComponentBase<PopupConfirmOrder>
	{
		[Parameter] public OrderDto Order { get; set; }
		[Parameter] public EditContext FormContext { get; set; }
		[Parameter] public EventCallback OnClose { get; set; }
		[Parameter] public EventCallback<CustomResponse> OnAfterConfirm { get; set; }
		[Inject] public IJSRuntime JSRuntime { get; set; }
		public List<ShippingAddressDto> ShippingAddressList { get; set; }
		public DisplayPopup SelectedPopup { get; set; }
		public CustomResponse ResponseModel { get; set; }

		public int TotalProduct { get; set; }
		/// <summary>
		/// Temporary shipping fee
		/// </summary>
		public decimal ShippingFee { get; set; } = 10000;
		/// <summary>
		/// Final price will inclue all anothers cast like VAT, trans...
		/// </summary>
		public decimal FinalPrice { get; set; }
		/// <summary>
		/// currently will set Default delivery time
		/// will have service calculate this later
		/// </summary>

		protected override void OnInitialized()
		{
			base.OnInitialized();
			SelectedPopup = DisplayPopup.OrderConfirm;
			Order.DeliveryDateTime = DateTime.Now;
		}

		protected override async Task OnInitializedAsync()
		{
			ShippingAddressList = await HttpClientServer
				.CustomGetAsync<List<ShippingAddressDto>>("api/ShippingAddress");
			if (ShippingAddressList != null)
			{
				Order.ShippingAddress = ShippingAddressList.FirstOrDefault();
			}
		}

		protected int GetTotalProduct()
		{
			return Order.OrderItemList.Sum(e => e.Quantity);
		}

		protected decimal GetTotalPrice()
		{
			decimal totalPrice = 0;
			Order.OrderItemList.ForEach(order =>
			{
				totalPrice += order.OrderItemPrice;
			});
			return totalPrice;
		}

		protected decimal GetFinalPrice()
		{
			return GetTotalPrice() + ShippingFee;
		}

		protected async Task HandleCloseConfirm()
		{
			await OnClose.InvokeAsync(null);
		}

		protected async Task HandleSubmitConfirm()
		{
			//await JSRuntime.InvokeVoidAsync("renderPaymentButton", 10);
			if (Order.ShippingAddress != null)
			{
				ResponseModel =
					await HttpClientServer.CustomPostAsync<OrderDto, CustomResponse>("api/Order", Order);
				await OnAfterConfirm.InvokeAsync(ResponseModel);
			}
		}

		[JSInvokable]
		public static void SendOrder()
		{
		}

		protected void HandleChangeShippingAddress()
		{
			SelectedPopup = DisplayPopup.ChangeAddress;
		}

		protected void HandleCloseChangeAddress()
		{
			SelectedPopup = DisplayPopup.OrderConfirm;
		}

		protected void HandleConfirmChangeAddress()
		{
			SelectedPopup = DisplayPopup.OrderConfirm;
		}
	}
}
