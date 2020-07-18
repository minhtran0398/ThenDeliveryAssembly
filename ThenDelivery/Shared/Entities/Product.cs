using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class Product
	{
		public int ProductId { get; set; }
		public int MerchantMenuId { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
		public string Description { get; set; }
		public int OrderCount { get; set; }
		public int FavoriteCount { get; set; }
		public decimal UnitPrice { get; set; }
		public string Image { get; set; }
	}
}
