using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Order
{
	public class OrderedItemListBase : CustomComponentBase<OrderedItemListBase>
	{
		[CascadingParameter] public List<OrderItem> OrderItemList { get; set; }

		protected decimal TotalPrice { get; set; }

		protected void HandleDecreaseQuantity(int orderId)
		{
			var orderItem = OrderItemList.Single(e => e.Id == orderId);
			if (orderItem.Quantity > 1)
			{
				orderItem.Quantity -= 1;
			}
			StateHasChanged();
		}

		protected void HandleIncreaseQuantity(int orderId)
		{
			OrderItemList.Single(e => e.Id == orderId).Quantity += 1;
			StateHasChanged();
		}

	}
}
