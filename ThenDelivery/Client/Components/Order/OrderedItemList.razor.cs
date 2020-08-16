using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Order
{
	public class OrderedItemListBase : CustomComponentBase<OrderedItemListBase>
	{
		[Parameter] public List<OrderItem> OrderItemList { get; set; }
		[Parameter] public EventCallback<List<OrderItem>> OnOrderConfirm { get; set; }

		protected decimal TotalPrice { get; set; }

		protected override void OnParametersSet()
		{
			UpdateTotalPrice();
		}

		protected async Task HandleNoteChanged(int orderId, ChangeEventArgs args)
		{
			string newValue = args.Value.ToString();
			var orderItem = OrderItemList.Single(e => e.Id == orderId);
			orderItem.Note = newValue;

			await InvokeAsync(StateHasChanged);
		}

		protected void HandleDecreaseQuantity(int orderId)
		{
			var orderItem = OrderItemList.Single(e => e.Id == orderId);
			if (orderItem.Quantity > 1)
			{
				orderItem.Quantity -= 1;
			}
			UpdateTotalPrice();
			StateHasChanged();
		}

		protected void HandleIncreaseQuantity(int orderId)
		{
			OrderItemList.Single(e => e.Id == orderId).Quantity += 1;
			UpdateTotalPrice();
			StateHasChanged();
		}

		private void UpdateTotalPrice()
		{
			TotalPrice = 0;
			OrderItemList.ForEach(order =>
			{
				TotalPrice += order.OrderItemPrice;
			});
		}

		protected async Task HandleConfirmOrder()
		{
			await OnOrderConfirm.InvokeAsync(OrderItemList);
		}
	}
}
