using System;
using System.Collections.Generic;
using System.Text;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Shared.Dtos
{
   public class EditProductVM
   {
      public EditProductVM()
      {
         ToppingList = new List<ToppingDto>();
      }
      public int Id { get; set; }
      public string Name { get; set; }
      public bool IsAvailable { get; set; }
      public string Description { get; set; }
      public int OrderCount { get; set; }
      public int FavoriteCount { get; set; }
      public decimal UnitPrice { get; set; }
      public string Image { get; set; }
      public List<ToppingDto> ToppingList { get; set; }

      public string ToppingListString
      {
         get
         {
            var stringBuilder = new StringBuilder();

            foreach (var topping in ToppingList)
            {
               stringBuilder.Append(topping.Name);
               stringBuilder.Append(", ");
            }

            if (ToppingList.Count == 0) return string.Empty;
            return stringBuilder.ToString()[0..^2];
         }
      }

      public void SetData(EditProductVM newData)
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

   public class EditMenuItemVM
   {
      public EditMenuItemVM()
      {
         ProductList = new List<EditProductVM>();
      }
      public int Id { get; set; }
      public int MerchantId { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public List<EditProductVM> ProductList { get; set; }

   }
   public class MenuIdComparer : IEqualityComparer<MenuItem>
   {
      public bool Equals(MenuItem x, MenuItem y)
      {
         return x.Id == y.Id;
      }

      public int GetHashCode(MenuItem obj)
      {
         //Check whether the object is null
         if (obj is null) return 0;

         return obj.Id.GetHashCode();
      }
   }
   public class ProductIdComparer : IEqualityComparer<Product>
   {
      public bool Equals(Product x, Product y)
      {
         return x.Id == y.Id;
      }

      public int GetHashCode(Product obj)
      {
         //Check whether the object is null
         if (obj is null) return 0;

         return obj.Id.GetHashCode();
      }
   }
}
