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

namespace ThenDelivery.Server.Application.MerchantMenuController.Queries
{
	public class GetMenuItemsByMerchantIdQuery
		: IRequest<IEnumerable<MenuItemDto>>
	{
		private readonly int _merchantId;

		public GetMenuItemsByMerchantIdQuery(int merchantId)
		{
			_merchantId = merchantId;
		}

		public class Handler
			: IRequestHandler<GetMenuItemsByMerchantIdQuery, IEnumerable<MenuItemDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetMenuItemsByMerchantIdQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext,
				ILogger<GetMenuItemsByMerchantIdQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<MenuItemDto>> Handle(
				GetMenuItemsByMerchantIdQuery request, CancellationToken cancellationToken)
			{
				var result = new List<MenuItemDto>();
				try
				{
					result = await (from menu in _dbContext.MenuItems
										 where menu.MerchantId == request._merchantId
										 select new MenuItemDto()
										 {
											 Name = menu.Name,
											 Description = menu.Description,
											 MerchantId = menu.MerchantId
										 }).ToListAsync();
				}
				catch (Exception ex)
				{
					_logger.LogError(ex.Message);
					return null;
				}
				return result;
			}
		}
	}
}
