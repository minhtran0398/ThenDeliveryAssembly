using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
   public class EditMerchantVM
   {
      public EditMerchantVM()
      {
         MenuItemList = new List<EditMenuItemVM>();
      }
      public int MerchantId { get; set; }
      public List<EditMenuItemVM> MenuItemList { get; set; }
   }
}
