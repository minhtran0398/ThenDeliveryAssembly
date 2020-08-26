using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
   public class EditMerchantVM
   {
      public EditMerchantVM()
      {
         MenuItemList = new List<MenuItemDto>();
         ProductList = new List<ProductDto>();
      }

      public int MerchantId { get; set; }
      public List<MenuItemDto> MenuItemList { get; set; }
      public List<ProductDto> ProductList { get; set; }
   }
}
