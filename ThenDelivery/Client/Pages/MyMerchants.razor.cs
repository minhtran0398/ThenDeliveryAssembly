using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Pages
{
   public class MyMerchantsBase : CustomComponentBase<MyMerchantsBase>
   {
      public List<MerchantDto> MyMerchantList { get; set; }
      public CustomResponse ResponseModel { get; set; }

      protected override async Task OnInitializedAsync()
      {
         await base.OnInitializedAsync();
         MyMerchantList =
            await HttpClientServer.CustomGetAsync<List<MerchantDto>>("api/Merchant/my");
      }

      protected void HandleMoveToOrderPage(int merchantId)
      {
         NavigationManager.NavigateTo($"/order-of-merchant/{merchantId}");
      }
   }
}
