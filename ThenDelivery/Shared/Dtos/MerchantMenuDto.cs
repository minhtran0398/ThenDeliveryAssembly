namespace ThenDelivery.Shared.Dtos
{
	public class MerchantMenuDto
	{
		public int MerchantId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public string DisplayText
		{
			get
			{
				return Name;
			}
		}
	}
}
