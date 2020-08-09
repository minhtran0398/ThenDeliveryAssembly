﻿namespace ThenDelivery.Shared.Dtos
{
	public class ToppingDto
	{
		public int Id { get; set; }
		public int ProductId { get; set; }
		public string Name { get; set; }
		public decimal UnitPrice { get; set; }

		public string Text
		{
			get
			{
				return string.Format("{0} - {1}", Name, UnitPrice);
			}
		}
	}
}
