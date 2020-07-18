using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class DistrictLevel : AuditableEntity
	{
		public byte DistrictLevelId { get; set; }
		public string Name { get; set; }
	}
}
