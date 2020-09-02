using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.Components.Enums;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Product
{
	public class AddProductBase : CustomComponentBase<AddProductBase>
	{
		#region Inject

		#endregion

		#region Parameters
		[Parameter] public int TargetMerchantId { get; set; }
		[Parameter] public EventCallback<PageAction> OnChangeTab { get; set; }
		#endregion

		#region Properties
		public List<MenuItemDto> MenuList { get; set; }
		public ObservableCollection<ProductDto> ProductList { get; set; }
		public ProductDto SelectedProduct { get; set; }
		public bool IsShowPopupAddProduct { get; set; }
		public bool IsEnableSaveButton { get; set; }
		public bool IsInsertMode { get; set; }
		#endregion

		#region Variables

		#endregion

		#region Life Cycle
		protected override async Task OnInitializedAsync()
		{
			MenuList = await HttpClientServer
				.CustomGetAsync<List<MenuItemDto>>($"api/menuitem?merchantId={TargetMerchantId}");
			var products =
				await HttpClientServer.CustomGetAsync<List<ProductDto>>($"api/product?merchantId={TargetMerchantId}");
			if (products is null || products.Count == 0)
			{
				IsInsertMode = true;
				ProductList = new ObservableCollection<ProductDto>();
			}
			else
			{
				IsInsertMode = false;
				IsEnableSaveButton = true;
				ProductList = new ObservableCollection<ProductDto>(products);
			}
			ProductList.CollectionChanged += HandleProductListChanged;
		}
		#endregion

		#region Events
		private void HandleProductListChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action == NotifyCollectionChangedAction.Add)
			{
				IsEnableSaveButton = true;
			}
			else if (e.Action == NotifyCollectionChangedAction.Remove)
			{
				if (MenuList.Count == 0)
					IsEnableSaveButton = false;
			}
		}

		protected void HandleShowProductForm()
		{
			SelectedProduct = new ProductDto() { IsAvailable = true };
			IsShowPopupAddProduct = true;
		}

		protected void HandleAddProduct(bool isShowForm)
		{
			AddProduct(SelectedProduct);
			IsShowPopupAddProduct = isShowForm;
			StateHasChanged();
		}

		protected void HandleCancelAddProduct(bool isShowForm)
		{
			IsShowPopupAddProduct = isShowForm;
		}

		protected async Task HandleTurnBack()
		{
			await OnChangeTab.InvokeAsync(PageAction.Previous);
		}

		/// <summary>
		/// Call api save data here
		/// </summary>
		protected async Task HandleSaveAndContinue()
		{
			if (IsInsertMode) await HttpClientServer.CustomPostAsync($"api/product", ProductList.AsEnumerable());
			else await HttpClientServer.CustomPutAsync($"api/product", ProductList.AsEnumerable());
			await OnChangeTab.InvokeAsync(PageAction.Next);
		}

		protected async Task HandleEditProduct(ProductDto product)
		{
			SelectedProduct = product;
			IsShowPopupAddProduct = true;
			await InvokeAsync(StateHasChanged);
		}

		protected async Task HandleDeleteProduct(int productId)
		{
			ProductList.RemoveFirst(e => e.Id == productId);
			await InvokeAsync(StateHasChanged);
		}
		#endregion

		#region Methods
		private void AddProduct(ProductDto product)
		{
			if (product != null)
			{
				if (product.Id == 0)
				{
					if (ProductList.Count == 0) product.Id = 1;
					else product.Id = ProductList.Max(e => e.Id) + 1;
					ProductList.Add(product);
				}
				//else
				//{
				//	var productToUpdate = ProductList.SingleOrDefault(e => e.Id == product.Id);
				//	productToUpdate = product;
				//}
				StateHasChanged();
			}
		}
		#endregion
	}
}
