using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class City : AuditableEntity
	{
		public string CityCode { get; set; }
		public string Name { get; set; }
		public byte CityLevelId { get; set; }
	}
}
