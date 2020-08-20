using System;
using System.Collections.Generic;
using System.Text;

namespace ThenDelivery.Shared.Dtos
{
	public class ProductDto
	{
		public ProductDto()
		{
			IsAvailable = true;
			ToppingList = new List<ToppingDto>();
		}

		public int Id { get; set; }
		public MenuItemDto MenuItem { get; set; }
		public MerchantDto Merchant { get; set; }
		public string Name { get; set; }
		public bool IsAvailable { get; set; }
		public string Description { get; set; }
		public int OrderCount { get; set; }
		public int FavoriteCount { get; set; }
		public decimal UnitPrice { get; set; }
		public string Image { get; set; }
		public List<ToppingDto> ToppingList { get; set; }
		public string ToppingListString
		{
			get
			{
				var stringBuilder = new StringBuilder();

				foreach (var topping in ToppingList)
				{
					stringBuilder.Append(topping.Name);
					stringBuilder.Append(", ");
				}

				if (ToppingList.Count == 0) return String.Empty;
				return stringBuilder.ToString()[0..^2];
			}
		}
	}
}
