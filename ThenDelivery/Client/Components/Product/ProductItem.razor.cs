using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class ProductItemBase : ComponentBase
	{
		[Parameter] public ProductDto Product { get; set; }
		[Parameter] public EventCallback<ProductDto> OnOrderProduct { get; set; }

		protected void HandleOnCancelTopping()
		{
			StateHasChanged();
		}

		protected async Task HandleAddProductOrder(ProductDto product)
		{
			await OnOrderProduct.InvokeAsync(product);
			await InvokeAsync(StateHasChanged);
		}
	}
}
