using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Entities;
using ThenDelivery.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace ThenDelivery.Server.Application.OrderController.Commands
{
	public class UpdateOrderStatusCommand : IRequest<Unit>
	{
		private readonly int _orderId;
		private readonly OrderStatus _orderStatus;
		private readonly string _shipperId;

		public UpdateOrderStatusCommand(int orderId, OrderStatus orderStatus, string shipperId)
		{
			_orderId = orderId;
			_orderStatus = orderStatus;
			_shipperId = shipperId;
		}

		public class Handler : IRequestHandler<UpdateOrderStatusCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<UpdateOrderStatusCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<UpdateOrderStatusCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					User shipper = await _dbContext.Users.SingleOrDefaultAsync(e => e.Id == request._shipperId);
					if (shipper is null)
					{
						throw new Exception("ShipperId is not valid");
					}

					Order order = await _dbContext.Orders.SingleOrDefaultAsync(e => e.Id == request._orderId);
					if (order is null)
					{
						throw new Exception("Order doesnt exist");
					}
					order.Status = (byte)request._orderStatus;
					order.ShipperId = request._shipperId;

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
		}
	}
}
