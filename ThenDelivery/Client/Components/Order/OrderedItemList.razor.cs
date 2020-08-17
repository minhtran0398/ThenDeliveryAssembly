using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Order
{
   public class OrderedItemListBase : CustomComponentBase<OrderedItemListBase>
   {
      [Parameter] public OrderDto Order { get; set; }
      [Parameter] public EditContext FormContext { get; set; }
      [Parameter] public EventCallback OnOrderConfirm { get; set; }

      protected decimal TotalPrice { get; set; }

      protected override void OnParametersSet()
      {
         UpdateTotalPrice();
      }

      protected async Task HandleNoteChanged(int orderId, ChangeEventArgs args)
      {
         string newValue = args.Value.ToString();
         var orderItem = Order.OrderItemList.Single(e => e.Id == orderId);
         orderItem.Note = newValue;

         await InvokeAsync(StateHasChanged);
      }

      protected void HandleDecreaseQuantity(int orderId)
      {
         var orderItem = Order.OrderItemList.Single(e => e.Id == orderId);
         if (orderItem.Quantity > 1)
         {
            orderItem.Quantity -= 1;
         }
         UpdateTotalPrice();
         StateHasChanged();
      }

      protected void HandleIncreaseQuantity(int orderId)
      {
         Order.OrderItemList.Single(e => e.Id == orderId).Quantity += 1;
         UpdateTotalPrice();
         StateHasChanged();
      }

      private void UpdateTotalPrice()
      {
         TotalPrice = 0;
         Order.OrderItemList.ForEach(order =>
         {
            TotalPrice += order.OrderItemPrice;
         });
      }

      protected async Task HandleConfirmOrder()
      {
         await OnOrderConfirm.InvokeAsync(null);
      }
   }
}
