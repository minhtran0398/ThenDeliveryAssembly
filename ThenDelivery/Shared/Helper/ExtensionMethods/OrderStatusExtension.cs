using ThenDelivery.Shared.Enums;
using System;

namespace ThenDelivery.Shared.Helper.ExtensionMethods
{
	public static class OrderStatusExtension
	{
		public static string GetStringValue(this OrderStatus status)
		{
			return status switch
			{
				OrderStatus.OrderSuccess => "Đặt hàng thành công",
				OrderStatus.ShipperAccept => "Đã nhận đơn",
				OrderStatus.Delivery => "Đang giao",
				OrderStatus.DeliverySuccess => "Giao hàng thành công",
				OrderStatus.Cancel => "Đơn đã hủy",
				_ => throw new ArgumentException("invalid GetStringValue enum value", nameof(status)),
			};
		}

		public static string GetStringNextAction(this OrderStatus status)
		{
			return status switch
			{
				OrderStatus.OrderSuccess => "Chọn đơn hàng này",
				OrderStatus.ShipperAccept => "Tiến hành giao hàng",
				OrderStatus.Delivery => "Hoàn tất đơn hàng",
				_ => throw new ArgumentException("invalid GetStringNextAction enum value", nameof(status)),
			};
		}

		public static OrderStatus GetNextStatus(this OrderStatus status)
		{
			return status switch
			{
				OrderStatus.None => OrderStatus.OrderSuccess,
				OrderStatus.OrderSuccess => OrderStatus.ShipperAccept,
				OrderStatus.ShipperAccept => OrderStatus.Delivery,
				OrderStatus.Delivery => OrderStatus.DeliverySuccess,
				_ => throw new ArgumentException("Invalid GetNextStatus Enum Value", nameof(status)),
			};
		}

		public static string GetCssClass(this OrderStatus status)
		{
			return status switch
			{
				OrderStatus.ShipperAccept => "badge badge-pill badge-info",
				OrderStatus.Delivery => "badge badge-pill badge-primary",
				OrderStatus.DeliverySuccess => "badge badge-pill badge-success",
				OrderStatus.OrderSuccess => "badge badge-pill badge-secondary",
				OrderStatus.Cancel => "badge badge-pill badge-danger",
				_ => throw new ArgumentException("invalid enum value", nameof(status)),
			};
		}

		public static bool CanCancel(this OrderStatus status)
      {
			return status switch
			{
				OrderStatus.None => true,
				OrderStatus.OrderSuccess => true,
				OrderStatus.ShipperAccept => true,
				OrderStatus.Delivery => false,
				OrderStatus.DeliverySuccess => false,
				OrderStatus.Cancel => false,
				_ => throw new ArgumentException("Invalid GetNextStatus Enum Value", nameof(status)),
			};
		}
	}
}