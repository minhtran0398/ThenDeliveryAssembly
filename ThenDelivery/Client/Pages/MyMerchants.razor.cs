using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Pages
{
   public class MyMerchantsBase : CustomComponentBase<MyMerchantsBase>
   {
      public List<MerchantDto> MyMerchantList { get; set; }

      protected override async Task OnInitializedAsync()
      {
         await base.OnInitializedAsync();
         MyMerchantList =
            await HttpClientServer.CustomGetAsync<List<MerchantDto>>("api/Merchant/my");
      }
   }
}
