using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class Product : AuditableEntity
	{
		public int Id { get; set; }
		public int MenuItemId { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
		public string Description { get; set; }
		public int OrderCount { get; set; }
		public int FavoriteCount { get; set; }
		public decimal UnitPrice { get; set; }
		public string Image { get; set; }

		public void SetData(Product product)
      {
			foreach (var item in this.GetType().GetProperties())
			{
				if (item.CanRead && item.CanWrite)
				{
					item.SetValue(this, product.GetType().GetProperty(item.Name).GetValue(product));
				}
			}
		}
	}
}
