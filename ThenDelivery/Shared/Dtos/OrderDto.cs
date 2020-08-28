using System;
using ThenDelivery.Shared.Enums;
using System.Collections.Generic;

namespace ThenDelivery.Shared.Dtos
{
	public class OrderDto
	{
		public OrderDto()
		{
			OrderItemList = new List<OrderItem>();
			DeliveryDateTime = DateTime.Now;
		}

		public int Id { get; set; }
		public UserDto User { get; set; }
		public UserDto Shipper { get; set; }
		public MerchantDto Merchant { get; set; }
		public ShippingAddressDto ShippingAddress { get; set; }
		public DateTime OrderDateTime { get; set; }
		public DateTime DeliveryDateTime { get; set; }
		public string Note { get; set; }
		public byte ReceiveVia { get; set; }
		public OrderStatus Status { get; set; }

		public List<OrderItem> OrderItemList { get; set; }

		public decimal TotalAmount
		{
			get
			{
				decimal result = 0;
				OrderItemList.ForEach(item =>
				{
					result += item.OrderItemPrice;
				});
				return result;
			}
		}
	}
}
