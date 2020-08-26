using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
      public EditMerchantVM FormModel { get; set; }
      public bool IsShowPopupAddProduct { get; set; }
      public ProductDto SelectedProduct { get; set; }

      protected override async Task OnInitializedAsync()
      {
         await base.OnInitializedAsync();
         var response = await HttpClientServer.GetAsync($"api/merchant/isExist?merchantId={MerchantId}");
         if(response.IsSuccessStatusCode == false)
         {
            NavigationManager.NavigateTo("/");
         }
         bool isExist = await response.Content.ReadFromJsonAsync<bool>();
         if(isExist == false)
         {
            NavigationManager.NavigateTo("/");
         }
         FormModel = new EditMerchantVM();
         FormModel.MenuItemList =
            await HttpClientServer.CustomGetAsync<List<MenuItemDto>>($"api/menuitem?merchantId={MerchantId}");
         FormModel.ProductList =
            await HttpClientServer.CustomGetAsync<List<ProductDto>>($"api/product?merchantId={MerchantId}");
      }

      protected void HandleAddProduct(ProductDto product)
      {
         product.Merchant = new MerchantDto() { Id = MerchantId };
         AddProduct(product);
         IsShowPopupAddProduct = false;
         StateHasChanged();
      }

      private void AddProduct(ProductDto product)
      {
         if (product != null)
         {
            if (product.Id == 0)
            {
               if (FormModel.ProductList.Count == 0) product.Id = 1;
               else product.Id = FormModel.ProductList.Max(e => e.Id) + 1;
               FormModel.ProductList.Add(product);
            }
            else
            {
               var productToUpdate = FormModel.ProductList.SingleOrDefault(e => e.Id == product.Id);
               productToUpdate = product;
            }
            StateHasChanged();
         }
      }

      protected async Task HandleValidSubmit()
      {
         if(FormContext.Validate())
         {

         }
      }
   }
}
