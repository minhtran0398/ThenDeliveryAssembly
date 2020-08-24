using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.Components;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;

namespace ThenDelivery.Client.Pages
{
   public class MerchantOrderListBase : CustomComponentBase<MerchantOrderList>
   {
      [Parameter] public int MerchantId { get; set; }
      public List<OrderDto> OrderList { get; set; }
      
      protected string GetColorCssOrderStatus(OrderStatus status)
      {
         return status switch
         {
            OrderStatus.ShipperAccept => "bg-info disabled text-white",
            OrderStatus.Delivery => "bg-success disabled text-white",
            OrderStatus.DeliverySuccess => "bg-secondary disabled text-white",
            _ => string.Empty,
         };
      }

      protected override async Task OnInitializedAsync()
      {
         await base.OnInitializedAsync();
         OrderList = await HttpClientServer
            .CustomGetAsync<List<OrderDto>>($"api/order/order-of-merchant?merchantId={MerchantId}");
      }
   }
}
