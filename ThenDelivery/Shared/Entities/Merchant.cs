using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class Merchant : AuditableEntity
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Avatar { get; set; }
		public string CoverPicture { get; set; }
		public string TaxCode { get; set; }
		public string PhoneNumber { get; set; }
		public string OpenTime { get; set; }
		public string CloseTime { get; set; }
		public string Description { get; set; }
		public string SearchKey { get; set; } // Split by ","
		public string CityCode { get; set; }
		public string DistrictCode { get; set; }
		public string WardCode { get; set; }
		public string HouseNumber { get; set; }
      public byte Status { get; set; }
   }
}
