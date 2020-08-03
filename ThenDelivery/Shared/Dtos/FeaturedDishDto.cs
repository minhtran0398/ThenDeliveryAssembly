namespace ThenDelivery.Shared.Dtos
{
	public class FeaturedDishDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayText
		{
			get
			{
				return Name;
			}
		}
	}
}
