using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class Ward : AuditableEntity
	{
		public string WardCode { get; set; }
		public string DistrictCode { get; set; }
		public string Name { get; set; }
		public byte WardLevelId { get; set; }
	}
}
