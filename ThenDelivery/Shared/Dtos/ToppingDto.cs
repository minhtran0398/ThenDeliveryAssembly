namespace ThenDelivery.Shared.Dtos
{
	public class ToppingDto
	{
      public ToppingDto(ToppingDto toppingDto)
      {
			SetData(toppingDto);
      }

      public ToppingDto()
      {

      }

		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }
      public bool IsDelete { get; set; }
      public bool IsCreateNew { get; set; }

      public string Text
		{
			get
			{
				return string.Format("{0} - {1:N0}", Name, UnitPrice);
			}
		}

		public void SetData(ToppingDto newData)
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
