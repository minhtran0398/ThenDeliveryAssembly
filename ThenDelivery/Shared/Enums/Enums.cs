namespace ThenDelivery.Shared.Enums
{
	public enum OrderStatus
	{
		None = 0,
		OrderSuccess = 1,
		ShipperAccept = 2,
		Delivery = 3,
		DeliverySuccess = 4,
		Cancel = 5
	}

	public enum MerchantStatus
   {
		NotApproved = 1,
		Approved = 2,
		Closed = 3
	}
}