namespace ThenDelivery.Shared.Entities
{
	public class Topping
	{
		public int ToppingId { get; set; }
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
