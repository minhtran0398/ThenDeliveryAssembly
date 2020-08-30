using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;
using System.Linq;

namespace ThenDelivery.Server.Application.OrderController.Commands
{
	public class InsertOrderCommand : IRequest<Unit>
	{
		private readonly OrderDto _orderDto;

		public InsertOrderCommand(OrderDto orderDto)
		{
			_orderDto = orderDto;
		}

		public class Handler : IRequestHandler<InsertOrderCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<InsertOrderCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<InsertOrderCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(InsertOrderCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					if (string.IsNullOrWhiteSpace(request._orderDto.User.Id))
					{
						throw new Exception("UserId is not valid");
					}
					var userOrder = await _dbContext.Users.SingleOrDefaultAsync(e => e.Id == request._orderDto.User.Id);
					if (userOrder.EmailConfirmed == false)
					{
						throw new ArgumentException("Vui lòng xác nhận email để đặt hàng");
					}

					var shippingAddressListDb = _dbContext.ShippingAddresses.Where(e => e.UserId == request._orderDto.User.Id);
					// insert when user create new shipping address
					var shippingAddress = request._orderDto.ShippingAddress;
					if (shippingAddressListDb.Any(e => e.Id == shippingAddress.Id) == false)
					{
						var shippingAddressToInsert = new ShippingAddress()
						{
							Id = 0,
							CityCode = shippingAddress.City.CityCode,
							DistrictCode = shippingAddress.District.DistrictCode,
							FullName = shippingAddress.FullName,
							HouseNumber = shippingAddress.HouseNumber,
							PhoneNumber = shippingAddress.PhoneNumber,
							UserId = request._orderDto.User.Id,
							WardCode = shippingAddress.Ward.WardCode
						};
						await _dbContext.ShippingAddresses.AddAsync(shippingAddressToInsert);
						await _dbContext.SaveChangesAsync();
						shippingAddress.Id = shippingAddressToInsert.Id;
					}
					else
					{
						var shippingAddressToUpdate = await _dbContext.ShippingAddresses
																.SingleOrDefaultAsync(e => e.Id == shippingAddress.Id);
						shippingAddressToUpdate.CityCode = shippingAddress.City.CityCode;
						shippingAddressToUpdate.DistrictCode = shippingAddress.District.DistrictCode;
						shippingAddressToUpdate.WardCode = shippingAddress.Ward.WardCode;
						shippingAddressToUpdate.FullName = shippingAddress.FullName;
						shippingAddressToUpdate.HouseNumber = shippingAddress.HouseNumber;
						shippingAddressToUpdate.PhoneNumber = shippingAddress.PhoneNumber;
					}

					Order orderToInsert = GetOrderData(request._orderDto, shippingAddress.Id);
					await _dbContext.Orders.AddAsync(orderToInsert);
					await _dbContext.SaveChangesAsync();

					foreach (var orderItem in request._orderDto.OrderItemList)
					{
						var orderDetailToInsert = GetOrderDetailData(orderItem, orderToInsert.Id);
						await _dbContext.OrderDetails.AddAsync(orderDetailToInsert);
						await _dbContext.SaveChangesAsync();
						await _dbContext.OrderDetailToppings
							.AddRangeAsync(GetODToppingData(orderItem.SelectedToppingList, orderDetailToInsert.Id));
					}

					await _dbContext.SaveChangesAsync();
					await trans.CommitAsync();
				}
				catch (ArgumentException)
				{
					throw;
				}
				catch (Exception ex)
				{
					await trans.RollbackAsync();
					_logger.LogError(ex, ex.Message);
					throw;
				}
				return Unit.Value;
			}

			private Order GetOrderData(OrderDto orderDto, int newShippingAddressId)
			{
				var order = new Order()
				{
					DeliveryDateTime = orderDto.DeliveryDateTime,
					Note = orderDto.Note,
					OrderDateTime = DateTime.Now,
					ReceiveVia = orderDto.ReceiveVia,
					ShipperId = string.Empty,
					ShippingAddressId = newShippingAddressId,
					UserId = orderDto.User.Id,
					MerchantId = orderDto.Merchant.Id
				};
				return order;
			}

			private OrderDetail GetOrderDetailData(OrderItem orderItem, int orderId)
			{
				return new OrderDetail()
				{
					Id = 0,
					Note = orderItem.Note,
					OrderId = orderId,
					ProductId = orderItem.OrderProduct.Id,
					Quantity = orderItem.Quantity,
					UnitPrice = orderItem.OrderProduct.UnitPrice
				};
			}

			private IEnumerable<OrderDetailTopping> GetODToppingData
				(IEnumerable<ToppingDto> toppingDtoList, int orderDetailId)
			{
				foreach (var toppingDto in toppingDtoList)
				{
					yield return new OrderDetailTopping()
					{
						ToppingId = toppingDto.Id,
						OrderDetailId = orderDetailId,
						ToppingPrice = toppingDto.UnitPrice
					};
				}
			}
		}
	}
}
