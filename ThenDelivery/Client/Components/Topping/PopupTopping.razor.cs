using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Topping
{
	public class PopupToppingBase : CustomComponentBase<PopupToppingBase>
	{
		[Parameter] public ProductDto Product { get; set; }
		[Parameter] public EventCallback<OrderItem> OnAddOrderItem { get; set; }
		[Parameter] public EventCallback OnCancel { get; set; }

		protected IEnumerable<ToppingDto> SelectedToppingList { get; set; }
		protected short Quantity { get; set; }

		protected override void OnParametersSet()
		{
			SetInitState();
		}

		protected void HandleDecreaseQuantity()
		{
			if (Quantity > 1)
				Quantity -= 1;
			StateHasChanged();
		}

		protected void HandleIncreaseQuantity()
		{
			Quantity += 1;
			StateHasChanged();
		}

		protected async Task HandleCancel()
		{
			SetInitState();
			await OnCancel.InvokeAsync(null);
		}

		protected async Task HandleAddOrder()
		{
			var orderitem = new OrderItem()
			{
				OrderProduct = Product,
				Quantity = Quantity,
				SelectedToppingList = SelectedToppingList.ToList(),
			};
			SetInitState();

			await OnAddOrderItem.InvokeAsync(orderitem);
		}

		private void SetInitState()
		{
			Quantity = 1;
			SelectedToppingList = new List<ToppingDto>();
			StateHasChanged();
		}
	}
}
