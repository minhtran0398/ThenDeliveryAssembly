namespace ThenDelivery.Shared.Dtos
{
	public class MerchantTypeDto
	{
		public int MerchantTypeId { get; set; }
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
