using Microsoft.AspNetCore.Components;
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
		public MerchantDto Merchant { get; set; }
		public List<ProductDto> ProductListFull { get; set; }
		public List<ProductDto> ProductListMenu { get; set; }
		public List<MenuItemDto> MenuItemList { get; set; }
		public MenuItemDto SelectedMenu { get; set; }

		protected override async Task OnInitializedAsync()
		{
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
	}
}
