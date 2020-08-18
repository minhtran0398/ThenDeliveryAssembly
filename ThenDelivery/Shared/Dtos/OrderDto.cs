using System;
using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
	public class OrderDto
	{
		public OrderDto()
		{
			OrderItemList = new List<OrderItem>();
		}

		public int Id { get; set; }
		public string UserId { get; set; }
		public string ShipperId { get; set; }
		public ShippingAddressDto ShippingAddress { get; set; }
		public DateTime OrderDateTime { get; set; }
		public DateTime DeliveryDateTime { get; set; }
		public string Note { get; set; }
		public byte ReceiveVia { get; set; }
		public byte Status { get; set; }

		public List<OrderItem> OrderItemList { get; set; }
	}
}
