using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ThenDelivery.Client.Components.Merchant;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class AddProductFormBase : CustomComponentBase<AddProductFormBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public EventCallback<ProductDto> OnSaveProduct { get; set; }
		[Parameter] public EventCallback<bool> OnCancel { get; set; }
		#endregion

		#region Properties
		public ProductDto ProductModel { get; set; }
		public List<MerchantMenuDto> MenuList { get; set; }
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

		}

		protected void HandleSelectedMenuChanged(MerchantMenuDto newValue)
		{

		}

		protected void HandleUnitPriceChanged(decimal newValue)
		{

		}

		protected void HandleDescriptionChanged(string newValue)
		{
			
		}
		#endregion

		#region Methods
		#endregion
	}
}
