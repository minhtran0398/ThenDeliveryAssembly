using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

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
		public List<ShippingAddressDto> ShippingAddressList { get; set; }
		public DisplayPopup SelectedPopup { get; set; }

		public decimal TotalPrice { get; set; }
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
		public DateTime DeleveryDateTime { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			SelectedPopup = DisplayPopup.OrderConfirm;
			DeleveryDateTime = DateTime.Now;

			UpdateTotalPrice();
			UpdateFinalPrice();
			UpdateTotalProduct();
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

		private void UpdateTotalProduct()
		{
			TotalProduct = Order.OrderItemList.Sum(e => e.Quantity);
		}

		private void UpdateTotalPrice()
		{
			TotalPrice = 0;
			Order.OrderItemList.ForEach(order =>
			{
				TotalPrice += order.OrderItemPrice;
			});
		}

		private void UpdateFinalPrice()
		{
			FinalPrice = TotalPrice + ShippingFee;
		}

		protected async Task HandleCloseConfirm()
		{
			await OnClose.InvokeAsync(null);
		}

		protected async Task HandleSubmitConfirm()
		{
			Logger.LogInformation(JsonConvert.SerializeObject(Order));
			Logger.LogInformation(await HttpClientServer.CustomPostAsync("api/Order", Order));
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
			DeleveryDateTime = DateTime.Now;
			SelectedPopup = DisplayPopup.OrderConfirm;
		}
	}
}
