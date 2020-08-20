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

					Order orderToInsert = GetOrderData(request._orderDto);
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
				catch (Exception ex)
				{
					await trans.RollbackAsync();
					_logger.LogError(ex, ex.Message);
					throw;
				}
				return Unit.Value;
			}

			private Order GetOrderData(OrderDto orderDto)
			{
				var order = new Order()
				{
					DeliveryDateTime = orderDto.DeliveryDateTime,
					Note = orderDto.Note,
					OrderDateTime = DateTime.Now,
					ReceiveVia = orderDto.ReceiveVia,
					ShipperId = string.Empty,
					ShippingAddressId = orderDto.ShippingAddress.Id,
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
