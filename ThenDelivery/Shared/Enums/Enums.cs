namespace ThenDelivery.Shared.Enums
{
	public enum OrderStatus
	{
		None = 0,
		OrderSuccess = 1,
		MerchantAccept = 2,
		ShipperAccept = 3,
		Delivery = 4,
		DeliverySuccess = 5,
		Cancel = 6
	}

	public enum MerchantStatus
	{
		None = 0,
		NotApproved = 1,
		Approved = 2,
		Closed = 3,
		AdminClosed
	}
}