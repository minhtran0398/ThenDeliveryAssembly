using System;

namespace ThenDelivery.Shared.Entities
{
	public class ShippingAddress
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string CityCode { get; set; }
		public string DistrictCode { get; set; }
		public string WardCode { get; set; }
		public string HouseNumber { get; set; }
	}
}
