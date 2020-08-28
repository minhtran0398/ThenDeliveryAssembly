using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Client.Components
{
   public class OrderListCpnBase : CustomComponentBase<OrderListCpnBase>
   {
      [Parameter] public List<OrderDto> OrderList { get; set; }
      public CustomResponse ResponseModel { get; set; }
      public int Count { get; set; }

      protected override void OnInitialized()
      {
         base.OnInitialized();
         Count = 0;
      }

      protected override void OnAfterRender(bool firstRender)
      {
         Count = 0;
      }

      protected async Task HandleCancelOrder(OrderDto order)
      {
         ResponseModel = await HttpClientServer.CustomPutAsync("api/order/cancel", order);
         if (ResponseModel.IsSuccess)
         {
            order.Status = OrderStatus.Cancel;
         }
         ResponseModel.IsShowPopup = true;
         await InvokeAsync(StateHasChanged);
      }
   }
}