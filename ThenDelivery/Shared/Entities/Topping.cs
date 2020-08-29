using System;
using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class Topping : AuditableEntity
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }

      public void SetData(Topping newTopping)
      {
			foreach (var item in this.GetType().GetProperties())
			{
				if (item.CanRead && item.CanWrite)
				{
					item.SetValue(this, newTopping.GetType().GetProperty(item.Name).GetValue(newTopping));
				}
			}
		}
   }
}
