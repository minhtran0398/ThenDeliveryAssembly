using ThenDelivery.Shared.Common;

namespace ThenDelivery.Shared.Entities
{
	public class MenuItem : AuditableEntity
	{
		public int Id { get; set; }
		public int MerchantId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public void SetData(MenuItem newData)
      {
			foreach (var item in this.GetType().GetProperties())
			{
				if (item.CanRead && item.CanWrite)
				{
					item.SetValue(this, newData.GetType().GetProperty(item.Name).GetValue(newData));
				}
			}
		}
	}
}
