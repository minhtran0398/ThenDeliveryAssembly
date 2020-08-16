using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Client.Shared;
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

      protected MenuItemDto SelectedMenu { get; set; }
      protected bool IsShowPopupTopping { get; set; }
      protected bool IsShowPopupOrderConfirm { get; set; }
      protected ProductDto SelectedProduct { get; set; }
      public List<OrderItem> OrderItemList { get; set; }

      protected override void OnInitialized()
      {
         OrderItemList = new List<OrderItem>();
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
         foreach (var orderedItem in OrderItemList)
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
            orderItem.Id = OrderItemList.Max(e => e?.Id) + 1 ?? 1;
            OrderItemList.Add(orderItem);
         }
         StateHasChanged();
      }

      protected void HandleConfirmOrder(List<OrderItem> orderItemList)
      {
         IsShowPopupOrderConfirm = true;
      }
   }
}
