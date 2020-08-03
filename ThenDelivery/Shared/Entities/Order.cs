using System;
using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class Order : AuditableEntity
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string ShipperId { get; set; }
		public int? ShippingAddressId { get; set; }
		public DateTime OrderDateTime { get; set; }
		public string Note { get; set; }
		public byte ReceiveVia { get; set; }
	}
}
