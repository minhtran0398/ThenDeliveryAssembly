using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;

namespace ThenDelivery.Server.Application.OrderController.Commands
{
	public class UpdateOrderCommand : IRequest<Unit>
	{
		private readonly OrderDto _orderDto;

		public UpdateOrderCommand(OrderDto orderDto)
		{
			_orderDto = orderDto;
		}

		public class Handler : IRequestHandler<UpdateOrderCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<UpdateOrderCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateOrderCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					if (string.IsNullOrWhiteSpace(request._orderDto.User.Id))
					{
						throw new Exception("UserId is not valid");
					}

					// Update when user create new shipping address
					var shippingAddress = request._orderDto.ShippingAddress;
					if (shippingAddress.Id == 0)
					{
						var shippingAddressToUpdate = new ShippingAddress()
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
						await _dbContext.ShippingAddresses.AddAsync(shippingAddressToUpdate);
						await _dbContext.SaveChangesAsync();
						shippingAddress.Id = shippingAddressToUpdate.Id;
					}

					Order orderToUpdate = GetOrderData(request._orderDto, shippingAddress.Id);
					await _dbContext.Orders.AddAsync(orderToUpdate);
					await _dbContext.SaveChangesAsync();

					foreach (var orderItem in request._orderDto.OrderItemList)
					{
						var orderDetailToUpdate = GetOrderDetailData(orderItem, orderToUpdate.Id);
						await _dbContext.OrderDetails.AddAsync(orderDetailToUpdate);
						await _dbContext.SaveChangesAsync();
						await _dbContext.OrderDetailToppings
							.AddRangeAsync(GetODToppingData(orderItem.SelectedToppingList, orderDetailToUpdate.Id));
					}

					await _dbContext.SaveChangesAsync();
					await trans.CommitAsync();
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
					UserId = orderDto.User.Id
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
						OrderDetailId = orderDetailId
					};
				}
			}
		}
	}
}
