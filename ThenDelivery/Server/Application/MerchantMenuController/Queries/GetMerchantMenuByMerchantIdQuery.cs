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
	public class GetMerchantMenuByMerchantIdQuery
		: IRequest<IEnumerable<MerchantMenuDto>>
	{
		private readonly int _merchantId;

		public GetMerchantMenuByMerchantIdQuery(int merchantId)
		{
			_merchantId = merchantId;
		}

		public class Handler
			: IRequestHandler<GetMerchantMenuByMerchantIdQuery, IEnumerable<MerchantMenuDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetMerchantMenuByMerchantIdQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext,
				ILogger<GetMerchantMenuByMerchantIdQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<MerchantMenuDto>> Handle(
				GetMerchantMenuByMerchantIdQuery request, CancellationToken cancellationToken)
			{
				var result = new List<MerchantMenuDto>();
				try
				{
					result = await (from menu in _dbContext.MerchantMenues
										 where menu.MerchantId == request._merchantId
										 select new MerchantMenuDto()
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
