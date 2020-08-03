namespace ThenDelivery.Shared.Entities
{
	public class Topping
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
	}
}
