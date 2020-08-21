using ThenDelivery.Shared.Enums;
using System;

namespace ThenDelivery.Shared.Helper.ExtensionMethods
{
	public static class OrderStatusExtension
	{
		public static string GetString(this OrderStatus status)
		{
			return status switch
			{
				OrderStatus.ShipperAccept => "Đã nhận đơn",
				OrderStatus.Delivery => "Đang giao",
				OrderStatus.DeliverySuccess => "Giao hàng thành công",
				OrderStatus.OrderSuccess => "Đặt hàng thành công",
				_ => throw new ArgumentException("invalid enum value", nameof(status)),
			};
		}
	}
}