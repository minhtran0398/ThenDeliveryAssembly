using Newtonsoft.Json;
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
		public short Quantity { get; set; }
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
		public string OrderProductToppingName
		{
			get
			{
				if (SelectedToppingList?.Count > 0)
				{
					return String.Format("{0} - {1}", OrderProduct.Name, ToppingListString);
				}
				else
				{
					return OrderProduct.Name;
				}
			}
		}
		public decimal OrderItemPrice
		{
			get
			{
				var result = OrderProduct.UnitPrice * Quantity;
				foreach (var toppingItem in SelectedToppingList)
				{
					result += toppingItem.UnitPrice;
				}
				return result;
			}
		}

		public bool JsonEqualProductAndTopping(OrderItem another)
		{
			if (ReferenceEquals(this, another)) return true;
			if (another == null) return false;

			var thisJson = JsonConvert.SerializeObject(new
			{
				OrderProduct.Id,
				SelectedToppingList,
			});
			var anotherJson = JsonConvert.SerializeObject(new
			{
				another.OrderProduct.Id,
				another.SelectedToppingList
			});

			return thisJson == anotherJson;
		}
	}
}
