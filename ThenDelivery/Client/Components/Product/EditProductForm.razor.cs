using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
   public class EditProductFormBase : CustomComponentBase<EditProductFormBase>
   {
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public EventCallback<EditProductVM> OnSaveProduct { get; set; }
		[Parameter] public EventCallback<bool> OnCancel { get; set; }
		[Parameter] public EditProductVM ProductModel { get; set; }
		#endregion

		#region Properties
		public EditContext FormContext { get; set; }
		public bool IsShowFormTopping { get; set; }
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			base.OnInitialized();
			if (ProductModel is null) ProductModel = new EditProductVM();
			FormContext = new EditContext(ProductModel);
		}
		#endregion

		#region Events
		protected async Task HandleOnSubmit()
		{
			if (FormContext.Validate())
			{
				await OnSaveProduct.InvokeAsync(ProductModel);
				ProductModel = new EditProductVM();
			}
		}

		protected async Task HandleOnCancel()
		{
			await OnCancel.InvokeAsync(false);
			ProductModel = new EditProductVM();
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
