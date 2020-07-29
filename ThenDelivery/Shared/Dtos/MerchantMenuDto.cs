using System;

namespace ThenDelivery.Shared.Dtos
{
	public class MerchantMenuDto
	{
		public MerchantMenuDto()
		{
			Description = String.Empty;
		}
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
