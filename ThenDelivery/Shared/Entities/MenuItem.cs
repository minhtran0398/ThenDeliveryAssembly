using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class MenuItem : AuditableEntity
	{
		public int Id { get; set; }
		public int MerchantId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}
