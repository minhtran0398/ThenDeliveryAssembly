using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Entities;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Exceptions;

namespace ThenDelivery.Server.Application.OrderController.Commands
{
	public class MerchantAcceptOrderCommand : IRequest<Unit>
	{
		private readonly int _orderId;

		public MerchantAcceptOrderCommand(int orderId)
		{
			_orderId = orderId;
		}

		public class Handler : IRequestHandler<MerchantAcceptOrderCommand, Unit>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<MerchantAcceptOrderCommand> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<MerchantAcceptOrderCommand> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<Unit> Handle(MerchantAcceptOrderCommand request, CancellationToken cancellationToken)
			{
				using var trans = _dbContext.Database.BeginTransaction();
				try
				{
					Order order = await _dbContext.Orders.SingleOrDefaultAsync(e => e.Id == request._orderId);
					if (order is null)
					{
						throw new Exception("Order doesnt exist");
					}

					if (order.Status == (byte)OrderStatus.MerchantAccept)
					{
						throw new UpdateOrderStatusException();
					}

					order.Status = (byte)OrderStatus.MerchantAccept;

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
