using System;
using System.Collections.Generic;
using System.Text;

namespace ThenDelivery.Shared.Dtos
{
	public class OrderItem
	{
		public int Id { get; set; }
		public ProductDto OrderProduct { get; set; }
		public int OrderId { get; set; }
		public List<ToppingDto> SelectedToppingList { get; set; }
		public int Quantity { get; set; }
		public string Note { get; set; }
		public string ToppingListString
		{
			get
			{
				var stringBuilder = new StringBuilder();

				foreach (var topping in SelectedToppingList)
				{
					stringBuilder.Append(topping.Name);
					stringBuilder.Append(", ");
				}

				if (SelectedToppingList.Count == 0) return String.Empty;
				return stringBuilder.ToString()[0..^2];
			}
		}
	}
}
