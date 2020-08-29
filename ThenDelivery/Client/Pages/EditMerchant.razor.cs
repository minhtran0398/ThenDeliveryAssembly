using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Pages
{
	public class EditMerchantBase : CustomComponentBase<EditMerchantBase>
	{
		[Parameter] public int MerchantId { get; set; }
		public EditContext FormContext { get; set; }
		public EditMerchantVM MerchantModel { get; set; }
		public EditMenuItemVM SelectedMenuItem { get; set; }
		public EditProductVM SelectedProduct { get; set; }
		public bool IsShowPopupAddProduct { get; set; }
		public CustomResponse ResponseModel { get; set; }

		protected override async Task OnInitializedAsync()
		{
			var response = await HttpClientServer.GetAsync($"api/merchant/isExist?merchantId={MerchantId}");
			if (response.IsSuccessStatusCode == false)
			{
				NavigationManager.NavigateTo("/", true);
			}
			bool isExist = await response.Content.ReadFromJsonAsync<bool>();
			if (isExist == false)
			{
				NavigationManager.NavigateTo("/", true);
			}

			MerchantModel = new EditMerchantVM() { MerchantId = MerchantId };
			MerchantModel.MenuItemList =
				await HttpClientServer.CustomGetAsync<List<EditMenuItemVM>>($"api/menuitem/edit?merchantId={MerchantId}");
			SelectedMenuItem = MerchantModel.MenuItemList.FirstOrDefault();
			FormContext = new EditContext(MerchantModel);

			await base.OnInitializedAsync();
		}

		protected async Task HandleEditMenuItemName(string newName, EditMenuItemVM menuItem)
		{
			if (string.IsNullOrWhiteSpace(newName) == false)
			{
				menuItem.Name = newName;
			}
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleChangeAvailable(bool newValue, EditProductVM product)
		{
			product.IsAvailable = newValue;
			StateHasChanged();
		}

		protected void HandleAddMenuItem()
		{
			int newId = 1;
			if (MerchantModel.MenuItemList?.Count > 0)
			{
				newId = MerchantModel.MenuItemList.Max(e => e.Id) + 1;
			}
			MerchantModel.MenuItemList.Add(new EditMenuItemVM()
			{
				Id = newId,
				Name = "Nhập tên nhóm mới",
				MerchantId = MerchantId,
				IsCreateNew = true
			});
			SelectedMenuItem = MerchantModel.MenuItemList.SingleOrDefault(e => e.Id == newId);
			StateHasChanged();
		}

		protected async Task HandleRemoveMenuItem(int menuItemId)
		{
			var menuItem = MerchantModel.MenuItemList.SingleOrDefault(s => s.Id == menuItemId);
			if (menuItem.IsCreateNew)
			{
				MerchantModel.MenuItemList.Remove(menuItem);
			}
			else
			{
				menuItem.IsDelete = true;
				for (int i = 0; i < menuItem.ProductList.Count; i++)
				{
					menuItem.ProductList[i].IsDelete = true;
				}
			}
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleClickMenuItem(EditMenuItemVM editMenu)
		{
			SelectedMenuItem = editMenu;
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleShowProductForm()
		{
			IsShowPopupAddProduct = true;
		}

		protected async Task HandleEditProduct(EditProductVM product)
		{
			SelectedProduct = product;
			IsShowPopupAddProduct = true;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleDeleteProduct(int productId)
		{
			var product = SelectedMenuItem.ProductList.SingleOrDefault(e => e.Id == productId);
			if (product.IsCreateNew)
			{
				SelectedMenuItem.ProductList.Remove(product);
			}
			else
			{
				product.IsDelete = true;
			}
			await InvokeAsync(StateHasChanged);
		}

		protected void HandleAddProduct(EditProductVM product)
		{
			AddProduct(product);
			IsShowPopupAddProduct = false;
			SelectedProduct = new EditProductVM() { IsCreateNew = true, IsAvailable = true };
			StateHasChanged();
		}

		protected void HandleCancelAddProduct(bool isShowForm = false)
		{
			IsShowPopupAddProduct = isShowForm;
			SelectedProduct = new EditProductVM();
		}

		private void AddProduct(EditProductVM product)
		{
			if (product != null)
			{
				if (product.Id == 0)
				{
					if (SelectedMenuItem.ProductList.Count == 0) product.Id = 1;
					else product.Id = SelectedMenuItem.ProductList.Max(e => e.Id) + 1;

					product.IsCreateNew = true;
					SelectedMenuItem.ProductList.Add(product);
				}
				else
				{
					var productToUpdate = SelectedMenuItem.ProductList.SingleOrDefault(e => e.Id == product.Id);
					productToUpdate = product;
				}
				StateHasChanged();
			}
		}

		protected bool IsEnableSaveButton()
		{
			return FormContext.Validate();
		}

		protected async Task HandleValidSubmit()
		{
			if (FormContext.Validate())
			{
				ResponseModel = await HttpClientServer.CustomPutAsync($"api/menuitem/edit", MerchantModel);
				ResponseModel.IsShowPopup = true;

				NavigationManager.NavigateTo($"/merchant-edit/{MerchantId}", false);
				// var newData = await HttpClientServer
				// 	.CustomGetAsync<List<EditMenuItemVM>>($"api/menuitem/edit?merchantId={MerchantId}");
				// MerchantModel.MenuItemList = newData;
				await InvokeAsync(StateHasChanged);
			}
		}
	}
}
