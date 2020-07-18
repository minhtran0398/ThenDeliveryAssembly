using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class District : AuditableEntity
	{
		public string DistrictCode { get; set; }
		public string Name { get; set; }
		public byte DistrictLevelId { get; set; }
	}
}
