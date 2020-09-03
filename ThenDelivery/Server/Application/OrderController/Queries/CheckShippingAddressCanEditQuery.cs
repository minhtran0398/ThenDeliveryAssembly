using ThenDelivery.Shared.Helper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Server.Persistence;
using ThenDelivery.Shared.Dtos;
using ThenDelivery.Shared.Enums;
using ThenDelivery.Shared.Helper.ExtensionMethods;

namespace ThenDelivery.Server.Application.OrderController.Queries
{
   public class CheckShippingAddressCanEditQuery : IRequest<bool>
   {
      private readonly int _shippingAddressId;

      public CheckShippingAddressCanEditQuery(int shippingAddressId)
      {
         _shippingAddressId = shippingAddressId;
      }

		public class Handler : IRequestHandler<CheckShippingAddressCanEditQuery, bool>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<CheckShippingAddressCanEditQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<CheckShippingAddressCanEditQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<bool> Handle(CheckShippingAddressCanEditQuery request,
				CancellationToken cancellationToken)
			{
				var result = false;
				try
				{
					var statusList = await (from o in _dbContext.Orders
										 where o.ShippingAddressId == request._shippingAddressId
										 select (OrderStatus)o.Status).ToListAsync();
					result = !statusList.Any(e => e.CanEdit() == false);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, ex.Message);
					throw;
				}
				return result;
			}
		}
	}
}
