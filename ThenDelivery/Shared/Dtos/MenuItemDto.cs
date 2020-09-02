namespace ThenDelivery.Shared.Dtos
{
   public class MenuItemDto
   {
      public int Id { get; set; }
      public int MerchantId { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }

      public string DisplayText
      {
         get
         {
            return Name;
         }
      }

      public void SetData(MenuItemDto newData)
      {
         foreach (var item in this.GetType().GetProperties())
         {
            if (item.CanRead && item.CanWrite)
            {
               item.SetValue(this, newData.GetType().GetProperty(item.Name).GetValue(newData));
            }
         }
      }
   }
}
