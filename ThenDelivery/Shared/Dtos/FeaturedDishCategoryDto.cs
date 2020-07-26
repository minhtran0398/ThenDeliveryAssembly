namespace ThenDelivery.Shared.Dtos
{
	public class FeaturedDishCategoryDto
	{
		public int FeaturedDishCategoryId { get; set; }
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
