namespace ThenDelivery.Shared.Common
{
	public class Const
	{
		public const int MaxMerTypePerMerchant = 2;
		public const int MaxFeaturedDishPerMerchant = 3;
		public const decimal MinProductUnitPrice = 1000;

		public class Role
		{
			public const string UserRole = "User";
			public const string ShipperRole = "Shipper";
			public const string MerchantRole = "Merchant";
			public const string AdministrationRole = "Administrator";
		}
	}
}
