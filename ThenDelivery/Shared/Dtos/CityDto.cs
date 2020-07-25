using System;

namespace ThenDelivery.Shared.Dtos
{
	public class CityDto
	{
		public string CityCode { get; set; }
		public string Name { get; set; }
		public byte CityLevelId { get; set; }
		public string CityLevelName { get; set; }
		public string DisplayText
		{
			get
			{
				return String.Format("{0} {1}", CityLevelName, Name);
			}
		}
	}
}
