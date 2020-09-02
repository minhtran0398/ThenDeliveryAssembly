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
		[Parameter] public EventCallback<bool> OnSaveProduct { get; set; }
		[Parameter] public EventCallback<bool> OnCancel { get; set; }
		[Parameter] public EditProductVM Product { get; set; }
		#endregion

		#region Properties
		public EditProductVM ProductModel { get; set; }
      public EditContext FormContext { get; set; }
		public bool IsShowFormTopping { get; set; }
		#endregion

		#region Life Cycle
		protected override void OnInitialized()
		{
			base.OnInitialized();
			if (Product is null)
			{
				ProductModel = new EditProductVM() { IsCreateNew = true, IsAvailable = true };
			}
			else
         {
				ProductModel = new EditProductVM();
				ProductModel.SetData(Product);
			}
			FormContext = new EditContext(ProductModel);
		}
		#endregion

		#region Events
		protected async Task HandleOnSubmit()
		{
			if (FormContext.Validate())
			{
				Product.SetData(ProductModel);
				await OnSaveProduct.InvokeAsync(false);
				ProductModel = null;
			}
		}

		protected async Task HandleOnCancel()
		{
			await OnCancel.InvokeAsync(false);
			ProductModel = null;
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

		protected bool IsEnableSubmit()
		{
			if (IsShowFormTopping) return false;
			return FormContext.Validate();
		}
		#endregion

		#region Methods
		#endregion
	}
}
