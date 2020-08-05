using Microsoft.AspNetCore.Components;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class ProductItemBase : ComponentBase
	{
		[Parameter] public ProductDto Product { get; set; }
	}
}
