namespace ThenDelivery.Shared.Dtos
{
	public class MerTypeDto
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
