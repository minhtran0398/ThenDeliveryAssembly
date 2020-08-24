using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
		[Parameter] public List<MenuItemDto> MenuList { get; set; }
		[Parameter] public EventCallback<ProductDto> OnSaveProduct { get; set; }
		[Parameter] public EventCallback<bool> OnCancel { get; set; }
		[Parameter] public ProductDto ProductModel { get; set; }
		#endregion

		#region Properties
      public EditContext FormContext { get; set; }
      public bool IsShowFormTopping { get; set; }
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			base.OnInitialized();
			if(ProductModel is null) ProductModel = new ProductDto();
			FormContext = new EditContext(ProductModel);
		}
		#endregion

		#region Events
		protected async Task HandleOnSubmit()
		{
			if(FormContext.Validate())
         {
				await OnSaveProduct.InvokeAsync(ProductModel);
				ProductModel = new ProductDto();
			}
		}

		protected async Task HandleOnCancel()
		{
			await OnCancel.InvokeAsync(false);
			ProductModel = new ProductDto();
		}

		protected void HandleCreateTopping()
		{
			IsShowFormTopping = true;
		}

		protected void HandleCancelCreateTopping()
		{
			IsShowFormTopping = false;
		}

		protected void HandleSubmitCreateTopping(List<ToppingDto> newValue)
		{
			ProductModel.ToppingList = newValue;
			IsShowFormTopping = false;
		}

		protected void HandleProductNameChanged(string newValue)
		{
			ProductModel.Name = newValue;
		}

		protected void HandleSelectedMenuChanged(MenuItemDto newValue)
		{
			ProductModel.MenuItem = newValue;
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
