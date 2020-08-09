using System;
using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
	public class OrderDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string ShipperId { get; set; }
		public int? ShippingAddressId { get; set; }
		public DateTime OrderDateTime { get; set; }
		public string Note { get; set; }
		public byte ReceiveVia { get; set; }

		public Dictionary<ProductDto, int> ProductList { get; set; }
	}
}
