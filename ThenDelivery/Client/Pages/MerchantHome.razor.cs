using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Pages
{
	public class MerchantHomeBase : CustomComponentBase<MerchantHomeBase>
	{
		[Parameter] public int MerchantId { get; set; }
		protected MerchantDto Merchant { get; set; }
		protected List<ProductDto> ProductListFull { get; set; }
		protected List<ProductDto> ProductListMenu { get; set; }
		protected List<MenuItemDto> MenuItemList { get; set; }
		protected OrderDto Order { get; set; }
		public EditContext FormContext { get; set; }
		protected MenuItemDto SelectedMenu { get; set; }
		protected bool IsShowPopupTopping { get; set; }
		protected bool IsShowPopupOrderConfirm { get; set; }
		protected ProductDto SelectedProduct { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			Order = new OrderDto();
			FormContext = new EditContext(Order);
		}

		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync();
			Merchant = await HttpClientAnonymous
				.CustomGetAsync<MerchantDto>($"api/merchant?merchantId={MerchantId}");
			ProductListFull = await HttpClientAnonymous
				.CustomGetAsync<List<ProductDto>>($"api/product?merchantId={MerchantId}");
			MenuItemList = await HttpClientAnonymous
				.CustomGetAsync<List<MenuItemDto>>($"api/menuitem?merchantId={MerchantId}");

			SelectedMenu = MenuItemList.Count > 0 ? MenuItemList[0] : null;
			ChangeProductListMenu(SelectedMenu);
		}

		protected void HandleOnChangeMenu(MenuItemDto newValue)
		{
			SelectedMenu = newValue;
			ChangeProductListMenu(SelectedMenu);
		}

		private void ChangeProductListMenu(MenuItemDto selectedMenu)
		{
			if (selectedMenu != null)
			{
				ProductListMenu = ProductListFull.Where(p => p.MenuItem.Id == selectedMenu.Id).ToList();
			}
		}

		/// <summary>
		/// Occur when click plus icon product item
		/// </summary>
		/// <param name="product"></param>
		protected void HandleOrderProduct(ProductDto product)
		{
			if (IsAuthenticated() == false)
			{
				NavigateToLogin();
			}
			else
			{
				SelectedProduct = product;
				IsShowPopupTopping = true;
			}
		}

		protected void HandleCancelOrder()
		{
			IsShowPopupTopping = false;
			StateHasChanged();
		}

		protected void HandleAddOrder(OrderItem orderItem)
		{
			IsShowPopupTopping = false;
			bool isAdd = true;
			foreach (var orderedItem in Order.OrderItemList)
			{
				if (orderedItem.JsonEqualProductAndTopping(orderItem))
				{
					orderedItem.Quantity += orderItem.Quantity;
					isAdd = false;
					break;
				}
			}
			if (isAdd)
			{
				orderItem.Id = Order.OrderItemList.Max(e => e?.Id) + 1 ?? 1;
				Order.OrderItemList.Add(orderItem);
			}
			StateHasChanged();
		}

		protected void HandleCloseConfirmPopup()
		{
			IsShowPopupOrderConfirm = false;
		}

		protected void HandleConfirmOrder()
		{
			IsShowPopupOrderConfirm = true;
		}
	}
}
