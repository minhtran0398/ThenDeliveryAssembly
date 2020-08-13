using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Order
{
	public class PopupConfirmOrderBase : CustomComponentBase<PopupConfirmOrder>
	{
		/*[Parameter] */public List<OrderItem> OrderItemList { get; set; }

		public decimal TotalPrice { get; set; }
		public int TotalProduct { get; set; }
		/// <summary>
		/// Temporary shipping fee
		/// </summary>
		public decimal ShippingFee { get; set; } = 10000;
		/// <summary>
		/// Final price will inclue all anothers cast like VAT, trans...
		/// </summary>
		public decimal FinalPrice { get; set; }
		public ShippingAddressDto ShippingAddress { get; set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			OrderItemList = new List<OrderItem>()
			{
				new OrderItem()
				{
					Quantity = 5,
					SelectedToppingList = new List<ToppingDto>()
					{
						new ToppingDto()
						{
							Name = "topping",
							UnitPrice = 2000,
						}
					},
					OrderProduct = new ProductDto()
					{
						Name = "món 1",
						UnitPrice = 100000,
					}
				},
				new OrderItem()
				{
					Quantity = 5,
					SelectedToppingList = new List<ToppingDto>()
					{
						new ToppingDto()
						{
							Name = "topping",
							UnitPrice = 2000,
						}
					},
					OrderProduct = new ProductDto()
					{
						Name = "món 1",
						UnitPrice = 100000,
					}
				},
				new OrderItem()
				{
					Quantity = 5,
					SelectedToppingList = new List<ToppingDto>()
					{
						new ToppingDto()
						{
							Name = "topping",
							UnitPrice = 2000,
						}
					},
					OrderProduct = new ProductDto()
					{
						Name = "món 1",
						UnitPrice = 100000,
					}
				},
				new OrderItem()
				{
					Quantity = 5,
					SelectedToppingList = new List<ToppingDto>()
					{
						new ToppingDto()
						{
							Name = "topping",
							UnitPrice = 2000,
						}
					},
					OrderProduct = new ProductDto()
					{
						Name = "món 1",
						UnitPrice = 100000,
					}
				},
				new OrderItem()
				{
					Quantity = 5,
					SelectedToppingList = new List<ToppingDto>()
					{
						new ToppingDto()
						{
							Name = "topping",
							UnitPrice = 2000,
						}
					},
					OrderProduct = new ProductDto()
					{
						Name = "món 1",
						UnitPrice = 100000,
					}
				},
			};
			ShippingAddress = new ShippingAddressDto()
			{
				Id = 1,
				City = new CityDto(),
				District = new DistrictDto(),
				HouseNumber = "123",
				Ward = new WardDto(),
				FullName = "Minh",
				PhoneNumber = "0123456789"
			};
			UpdateTotalPrice();
			UpdateFinalPrice();
			UpdateTotalProduct();
		}

		private void UpdateTotalProduct()
		{
			TotalProduct = OrderItemList.Sum(e => e.Quantity);
		}

		private void UpdateTotalPrice()
		{
			TotalPrice = 0;
			OrderItemList.ForEach(order =>
			{
				TotalPrice += order.OrderItemPrice;
			});
		}

		private void UpdateFinalPrice()
		{
			FinalPrice = TotalPrice + ShippingFee;
		}

		protected void HandleCloseConfirm()
		{

		}

		protected void HandleSubmitConfirm()
		{

		}
	}
}
