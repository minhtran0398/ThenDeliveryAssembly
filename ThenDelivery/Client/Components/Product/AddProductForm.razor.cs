using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class AddProductFormBase : CustomComponentBase<AddProductFormBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public List<MerchantMenuDto> MenuList { get; set; }
		[Parameter] public EventCallback<ProductDto> OnSaveProduct { get; set; }
		[Parameter] public EventCallback<bool> OnCancel { get; set; }
		#endregion

		#region Properties
		protected ProductDto ProductModel { get; set; }
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			ProductModel = new ProductDto();
		}
		#endregion

		#region Events
		protected async Task HandleOnSubmit()
		{
			await OnSaveProduct.InvokeAsync(ProductModel);
			ProductModel = new ProductDto();
		}

		protected async Task HandleOnCancel()
		{
			await OnCancel.InvokeAsync(false);
			ProductModel = new ProductDto();
		}

		protected void HandleProductNameChanged(string newValue)
		{
			ProductModel.Name = newValue;
		}

		protected void HandleSelectedMenuChanged(MerchantMenuDto newValue)
		{
			ProductModel.MerchantMenu = newValue;
		}

		protected void HandleUnitPriceChanged(decimal newValue)
		{
			ProductModel.UnitPrice = newValue;
		}

		protected void HandleDescriptionChanged(string newValue)
		{
			ProductModel.Description = newValue;
		}

		protected void HandleImageChanged(string newValue)
		{
			ProductModel.Image = newValue;
		}
		#endregion

		#region Methods
		#endregion
	}
}
