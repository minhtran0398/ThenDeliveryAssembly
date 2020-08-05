namespace ThenDelivery.Shared.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public int MenuItemId { get; set; }
		public int MerchantId { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
		public string Description { get; set; }
		public int OrderCount { get; set; }
		public int FavoriteCount { get; set; }
		public decimal UnitPrice { get; set; }
		public string Image { get; set; }
	}
}
