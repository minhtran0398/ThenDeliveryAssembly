using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class WardLevel : AuditableEntity
	{
		public byte WardLevelId { get; set; }
		public string Name { get; set; }
	}
}
