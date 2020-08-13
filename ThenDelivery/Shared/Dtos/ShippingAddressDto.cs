namespace ThenDelivery.Shared.Dtos
{
	public class ShippingAddressDto
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public CityDto City { get; set; }
		public DistrictDto District { get; set; }
		public WardDto Ward { get; set; }
		public string HouseNumber { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }

		public string AddressString
		{
			get
			{
				return string.Format("{0}, {1}, {2}, {3}",
					HouseNumber, Ward.DisplayText, District.DisplayText, City.DisplayText);
			}
		}
	}
}
