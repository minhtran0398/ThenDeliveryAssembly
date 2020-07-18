using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class CityLevel : AuditableEntity
	{
		public byte CityLevelId { get; set; }
		public string Name { get; set; }
	}
}
