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

		public ShippingAddressDto()
		{

		}

		public ShippingAddressDto(ShippingAddressDto addressDto)
		{
			SetData(addressDto);
		}

		public void SetData(ShippingAddressDto shippingAddress)
		{
			foreach (var item in this.GetType().GetProperties())
			{
				if (item.CanRead && item.CanWrite)
				{
					item.SetValue(this, shippingAddress.GetType().GetProperty(item.Name).GetValue(shippingAddress));
				}
			}
		}

		public string AddressString
		{
			get
			{
				if (HouseNumber != null && Ward != null && District != null && City != null)
				{
					return string.Format("{0}, {1}, {2}, {3}",
						HouseNumber, Ward.DisplayText, District.DisplayText, City.DisplayText);
				}
				else
				{
					return string.Empty;
				}
			}
		}
	}
}
