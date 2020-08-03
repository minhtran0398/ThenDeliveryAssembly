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

namespace ThenDelivery.Server.Application.MerchantTypeController.Queries
{
	public class GetAllMerchantTypeQuery : IRequest<IEnumerable<MerTypeDto>>
	{
		public class Handler : IRequestHandler<GetAllMerchantTypeQuery, IEnumerable<MerTypeDto>>
		{
			private readonly ThenDeliveryDbContext _dbContext;
			private readonly ILogger<GetAllMerchantTypeQuery> _logger;

			public Handler(ThenDeliveryDbContext dbContext, ILogger<GetAllMerchantTypeQuery> logger)
			{
				_dbContext = dbContext;
				_logger = logger;
			}

			public async Task<IEnumerable<MerTypeDto>> Handle(GetAllMerchantTypeQuery request,
				CancellationToken cancellationToken)
			{
				var result = new List<MerTypeDto>();
				try
				{
					result = await (from mt in _dbContext.MerchantTypes
										 select new MerTypeDto
										 {
											 Id = mt.Id,
											 Name = mt.Name,
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
